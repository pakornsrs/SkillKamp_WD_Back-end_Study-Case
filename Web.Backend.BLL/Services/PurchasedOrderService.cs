using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
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

        public ServiceResponseModel<DefaultResponseModel> PurchastOrder(int userId, int orderId, int paymentType, int cardId, int? couponId)
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

                    var paymentDetailResponse = paymentDetailService.InsertPaymentDetail(userId, orderDetail.Id, paymentType, (int)paymentStatus, (int)PaymentType.CreditCard == paymentType ? cardId : null);

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

                    if(couponId != null && couponId != 0)
                    {
                        var couponRespons = discountCouponService.GetCouponById(orderDetail.CouponId.Value);

                        if (!couponRespons.IsError)
                        {
                            var couponUpdateRespons = discountCouponService.UpdateStatusDiscountCoupon(couponId.Value);

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

    }
}
