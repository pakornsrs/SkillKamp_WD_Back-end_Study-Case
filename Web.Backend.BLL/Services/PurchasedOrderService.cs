using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.BLL.IServices;
using Web.Backend.BLL.ModelMappers;
using Web.Backend.BLL.UtilityMethods;
using Web.Backend.DAL;
using Web.Backend.DAL.Entities;
using Web.Backend.DTO;
using Web.Backend.DTO.CartItem;
using Web.Backend.DTO.Coupon;
using Web.Backend.DTO.Enums;
using Web.Backend.DTO.Inventories;
using Web.Backend.DTO.Orders;
using Web.Backend.DTO.PaymentDetail;
using Web.Backend.DTO.ProductDetails;
using Web.Backend.DTO.ProductReview;

namespace Web.Backend.BLL.Services
{
    public class PurchasedOrderService : IPurchasedOrderService
    {
        private readonly SkillkampWdStudyCaseDbContext dbContext;
        private readonly IOrderService orderService;
        private readonly IPaymentDetailService paymentDetailService;
        private readonly IDiscountCouponService discountCouponService;
        private readonly IPurchaseSessionService purchaseSessionService;
        private readonly IInventoryService inventoryService;


        private IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile<ModelMapper>()).CreateMapper();

        public PurchasedOrderService(SkillkampWdStudyCaseDbContext dbContext,
                               IOrderService orderService,
                               IPaymentDetailService paymentDetailService,
                               IDiscountCouponService discountCouponService,
                               IPurchaseSessionService purchaseSessionService,
                               IInventoryService inventoryService)
        {
            this.dbContext = dbContext;
            this.orderService = orderService;
            this.paymentDetailService = paymentDetailService;
            this.discountCouponService = discountCouponService;
            this.purchaseSessionService = purchaseSessionService;
            this.inventoryService = inventoryService;
        }

        public ServiceResponseModel<DefaultResponseModel> PurchastOrder(int userId, int orderId, int paymentType, int cardId, int addressId, string? addressDetail)
        {
            var response = new ServiceResponseModel<DefaultResponseModel>();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();
            var paymentStatus = ActionResult.None;
            
            OrderDTO orderDetail = null;
            PaymentDetailDTO paymentDetail = null;
            DiscountCouponDTO couponDetail = null;

            using (IDbContextTransaction transaction = this.dbContext.Database.BeginTransaction())
            {
                try
                {
                    // Query order detail

                    var orderDetailResponse = orderService.GetOrderDetail(orderId);

                    if (!orderDetailResponse.IsError && orderDetailResponse.Item != null)
                    {
                        orderDetail = orderDetailResponse.Item;
                    }
                    else
                    {
                        response.ErrorCode = "PO0001";
                        response.ErrorMessage = "Cannot find order detail.";

                        return response;
                    }

                    // Checs session

                    var sessionRespone = purchaseSessionService.CheckActiveSession(userId);

                    if(!sessionRespone.IsError && sessionRespone.Item == null)
                    {
                        response.ErrorCode = "PO0001";
                        response.ErrorMessage = "Session is expire.";

                        return response;
                    }
                    // Make a payment (mock alway pass anyway)

                    if((int)PaymentType.OnDelivery == paymentType)
                    {
                        // To do : add some service for pay with cash on delivery

                        paymentStatus = ActionResult.Success;
                    }
                    else if ((int)PaymentType.CreditCard == paymentType)
                    {
                        // To do : add some service for pay with credit card

                        paymentStatus = ActionResult.Success;
                    }
                    else if((int)PaymentType.Transfer == paymentType)
                    {
                        // To do : add some service for pay with transfer

                        paymentStatus = ActionResult.Success;
                    }

                    if (paymentStatus != ActionResult.Success)
                    {
                        response.ErrorCode = "PO0002";
                        response.ErrorMessage = "Cannot make a purchase.";

                        return response;
                    }

                    // Add payment detail

                    var paymentDetailResponse = paymentDetailService.InsertPaymentDetail(userId, orderDetail.Id, paymentType, (int)paymentStatus, addressId, addressDetail ,(int)PaymentType.CreditCard == paymentType ? cardId : null);

                    if (!paymentDetailResponse.IsError && paymentDetailResponse.Item != null)
                    {
                        paymentDetail = paymentDetailResponse.Item;
                    }
                    else{

                        response.ErrorCode = "PO0003";
                        response.ErrorMessage = "Cannot add payment detail.";

                        return response;
                    }

                    // insert purchasted order table

                    var detail = new PurchasedOrder();

                    detail.PaymentId = paymentDetail.Id;
                    detail.DiscountCouponId = orderDetail.CouponId;
                    detail.InvoiceId = null;
                    detail.OrderId = orderDetail.Id;
                    detail.UserId = userId;
                    detail.Amount = orderDetail.TotalAmount;

                    if(orderDetail.CouponId != null && orderDetail.CouponId != 0)
                    {
                        var couponRespons = discountCouponService.GetCouponById(orderDetail.CouponId.Value);

                        if (!couponRespons.IsError)
                        {
                            var couponUpdateRespons = discountCouponService.UpdateStatusDiscountCoupon(orderDetail.CouponId.Value);

                            if (couponRespons.IsError)
                            {
                                response.ErrorCode = "PO004";
                                response.ErrorMessage = couponRespons.ErrorMessage;

                                return response;
                            }

                            couponDetail = couponRespons.Item;
                            
                        }
                        else
                        {
                            couponDetail = null;
                        }
                    }

                    detail.DiscountAmount = couponDetail == null ? 0 : (couponDetail.PercentDiscount / 100) * orderDetail.TotalAmount;
                    detail.TotalAmount = detail.Amount - detail.DiscountAmount;
                    detail.CreateDate = tranDateTime.ToString();

                    dbContext.Set<PurchasedOrder>().Add(detail);
                    dbContext.SaveChanges();

                    // Remove item from inventory

                    var queryItem = (from q in dbContext.CartItems
                                     where q.SessionId == orderDetail.SessionId
                                     join prodDetail in dbContext.ProductDetails on q.ProductDetailId equals prodDetail.Id
                                     select new InventoryQtyDTO
                                     {
                                         InventoryId = prodDetail.InventoryId.Value,
                                         InventoryQty = q.Quantity.Value

                                     }).ToList();

                    if (queryItem == null || queryItem.Count <= 0)
                    {
                        response.ErrorCode = "PO0004";
                        response.ErrorMessage = "Cannot update inventory.";

                        return response;
                    }

                    var updateResult = inventoryService.UpdateInvertory(queryItem);

                    if (updateResult.IsError)
                    {
                        response.ErrorCode = "PO0004";
                        response.ErrorMessage = "Cannot update inventory.";

                        return response;
                    }

                    // Terminate session 

                    var terminateResponse = purchaseSessionService.TerminatePurchastSession(orderDetail.SessionId.Value);

                    // update order status

                    orderService.UpdateOrderStatus(orderDetail.Id, OrderStatus.Success);

                    var result = new DefaultResponseModel();
                    result.message = "Response success";

                    response.Item = result;
                    response.ErrorCode = "0000";
                    response.ErrorMessage = "Success";

                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    response.ErrorCode = "BE9999";
                    response.ErrorMessage = "Interal server error.";
                }
            }

            return response;
                
        }

        public ServiceResponseModel<List<PurchastedProductDTO>> GetPurchastItemHistory(int userId)
        {
            var response = new ServiceResponseModel<List<PurchastedProductDTO>>();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();


            using (IDbContextTransaction transaction = this.dbContext.Database.BeginTransaction())
            {
                try
                {
                    // get purchast record

                    var queryRecord = (from q in dbContext.PurchasedOrders
                                       where q.UserId == userId
                                       select q).ToList();

                    if(queryRecord == null || queryRecord.Count <= 0)
                    {
                        response.ErrorCode = "PH0001";
                        response.ErrorMessage = "No purchast history record.";
                    }

                    var orderIds = queryRecord.Select(obj => obj.OrderId).ToList();

                    // Get order detail

                    var orderResponse = orderService.GetOrderDetailList(orderIds);

                    if (orderResponse.Item == null || orderResponse.Item.Count <= 0)
                    {
                        response.ErrorCode = "PH0002";
                        response.ErrorMessage = "Cannot get order detail.";
                    }

                    var cartItems = new List<CartItemDTO>();

                    foreach(var order in orderResponse.Item)
                    {
                        var itemList = JsonConvert.DeserializeObject<List<CartItemDTO>>(order.CartItem);
                        foreach (var item in itemList)
                        {
                            item.OrderId = order.Id;
                            cartItems.Add(item);
                        }
                    }

                    // Get detail

                    var result = new List<PurchastedProductDTO>();

                    foreach(var item in cartItems)
                    {
                        var datetime = Convert.ToDateTime(queryRecord.Where(obj => obj.OrderId == item.OrderId).FirstOrDefault().CreateDate);

                        var queryDetail = (from detail in dbContext.ProductDetails
                                           where detail.Id == item.productDetailId
                                           join prod in dbContext.Products on detail.ProductId equals prod.Id
                                           join size in dbContext.ProductSizes on detail.SizeId equals size.Id
                                           join color in dbContext.ProductColors on detail.ColorId equals color.Id
                                           select new PurchastedProductDTO
                                           {
                                               OrderId = item.OrderId,
                                               ProductId = prod.Id,
                                               ProductDetailId = detail.Id,
                                               ProductNameTh = prod.ProductNameTh,
                                               ProductNameEn = prod.ProductNameEn,
                                               DescTh = prod.DescTh,
                                               DescEn = prod.DescEn,
                                               Price = detail.Price * item.Quantity,
                                               SizeId = size.Id,
                                               SizeDescTh = size.SizeDescTh,
                                               SizeDescEn = size.SizeDescEn,
                                               ColorId = color.Id,
                                               ColorDescTh = color.ColorNameTh,
                                               ColorDescEn = color.ColorNameEn,
                                               ImagePath = detail.ImagePath,
                                               ColorCode = color.ColorCode,
                                               PurchastDate = datetime.ToString("dd/MMM/yyyy"),
                                               PurchastTime = datetime.ToString("HH:mm:ss")
                                           }).FirstOrDefault();

                        if (queryDetail != null) result.Add(queryDetail);
                    }

                    // Find review

                    foreach (var item in result)
                    {
                        var queryReview = (from review in dbContext.ProductReviews
                                           where review.UserId == userId 
                                                 && review.ProductId == item.ProductId
                                                 && review.ProductDetailId == item.ProductDetailId
                                                 && review.OrderId == item.OrderId 
                                           select review).FirstOrDefault();

                        if(queryReview != null)
                        {
                            var review = mapper.Map<ProductReviewDTO>(queryReview);
                            item.ReviewDetail = review;
                        }
                        else
                        {
                            item.ReviewDetail = null;
                        }
                    }

                    response.Item = result;
                    response.ErrorCode = "0000";
                    response.ErrorMessage = "Success";

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    response.ErrorCode = "BE9999";
                    response.ErrorMessage = "Interal server error.";
                }
            }

            return response;
        }
    }
}
