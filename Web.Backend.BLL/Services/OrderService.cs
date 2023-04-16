using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
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
using Web.Backend.DTO.Orders;

namespace Web.Backend.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly SkillkampWdStudyCaseDbContext dbContext;
        private readonly IPurchaseSessionService purchaseSessionService;
        private readonly ICartItemService cartItemService;

        private IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile<ModelMapper>()).CreateMapper();

        public OrderService(SkillkampWdStudyCaseDbContext dbContext,
                            IPurchaseSessionService purchaseSessionService,
                            ICartItemService cartItemService)
        {
            this.dbContext = dbContext;
            this.purchaseSessionService = purchaseSessionService;
            this.cartItemService = cartItemService;
        }

        public ServiceResponseModel<OrderDTO> CreateProductOrder(int userId)
        {
            var response = new ServiceResponseModel<OrderDTO>();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            using (IDbContextTransaction transaction = this.dbContext.Database.BeginTransaction())
            {
                try
                {
                    // Get session

                    var sessionServiceResponse = purchaseSessionService.CheckActiveSession(userId);

                    if (!sessionServiceResponse.IsError && sessionServiceResponse.Item == null)
                    {
                        // Session expire

                        response.ErrorCode = "PO0001";
                        response.ErrorMessage = "Session is expired.";

                        return response;
                    }
                    else if (sessionServiceResponse.IsError)
                    {
                        response.ErrorCode = "PO0002";
                        response.ErrorMessage = "Check session error.";

                        return response;
                    }

                    // Get product

                    var sessionId = sessionServiceResponse.Item.Id;

                    var cartProductServiceResponse = cartItemService.GetAllCartItem(sessionId);

                    if (cartProductServiceResponse.IsError)
                    {
                        response.ErrorCode = "PO0003";
                        response.ErrorMessage = "Get cart item error.";

                        return response;
                    }
                    else if (!cartProductServiceResponse.IsError && cartProductServiceResponse.Item == null)
                    {
                        response.ErrorCode = "PO0004";
                        response.ErrorMessage = "Not found cart item.";

                        return response;
                    }

                    var productCart = cartProductServiceResponse.Item;

                    // Create order model

                    // query order

                    var orderQuery = (from q in dbContext.Orders
                                      where q.SessionId == sessionServiceResponse.Item.Id
                                      select q).FirstOrDefault();

                    if (orderQuery != null)
                    {
                        orderQuery.Amount = productCart.Sum(item => item.Price);
                        orderQuery.TotalAmount = orderQuery.Amount;
                        orderQuery.CartItem = JsonConvert.SerializeObject(productCart);
                        orderQuery.CouponId = 0;
                        orderQuery.UpdateDate = tranDateTime;
                        orderQuery.UpdateBy = "system";

                        dbContext.Set<Order>().Update(orderQuery);
                        dbContext.SaveChanges();

                        response.Item = mapper.Map<OrderDTO>(orderQuery);
                    }
                    else
                    {
                        var order = new Order();

                        order.SessionId = sessionId;
                        order.Amount = productCart.Sum(item => item.Price);
                        order.TotalAmount = order.Amount;
                        order.CartItem = JsonConvert.SerializeObject(productCart);
                        order.CouponId = 0;
                        order.CreateDate = tranDateTime;
                        order.UpdateDate = tranDateTime;
                        order.CreateBy = "system";
                        order.UpdateBy = "system";

                        dbContext.Set<Order>().Add(order);
                        dbContext.SaveChanges();

                        response.Item = mapper.Map<OrderDTO>(order);
                    }

                    response.ErrorCode = "0000";
                    response.ErrorMessage = "Success.";

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();

                    response.ErrorCode = "BE9999";
                    response.ErrorMessage = "Internal server error.";
                }
            }

            return response;
        }

        public ServiceResponseModel<OrderDTO> GetOrderDetail(int orderId)
        {
            var response = new ServiceResponseModel<OrderDTO>();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            try
            {
                var query = (from q in dbContext.Orders
                             where q.Id == orderId
                             select q).FirstOrDefault();

                var result = mapper.Map<OrderDTO>(query);

                response.Item = result;
                response.ErrorCode = "0000";
                response.ErrorMessage = "Success";
            }
            catch (Exception ex)
            {
                response.ErrorCode = "BE9999";
                response.ErrorMessage = "Internal server error.";
            }

            return response;
        }

        public ServiceResponseModel<List<OrderDTO>> GetOrderDetailList(List<int?> orderIds)
        {
            var response = new ServiceResponseModel<List<OrderDTO>>();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            try
            {
                var query = (from q in dbContext.Orders
                             where orderIds.Contains(q.Id)
                             select q).ToList();

                var result = mapper.Map<List<OrderDTO>>(query);

                response.Item = result;
                response.ErrorCode = "0000";
                response.ErrorMessage = "Success";
            }
            catch (Exception ex)
            {
                response.ErrorCode = "BE9999";
                response.ErrorMessage = "Internal server error.";
            }

            return response;
        }

        public ServiceResponseModel<OrderDTO> ApplyDiscountCoupon(int orderId, DiscountCouponDTO coupon)
        {
            var response = new ServiceResponseModel<OrderDTO>();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            try
            {
                // find order

                var orderQuery = (from q in this.dbContext.Orders
                                  where q.Id == orderId
                                  select q).FirstOrDefault();

                if(orderQuery == null)
                {
                    response.ErrorCode = "PO0005";
                    response.ErrorMessage = "Not found order.";

                    return response;
                }

                // Update order

                //orderQuery.TotalAmount = orderQuery.TotalAmount * (1 - ((decimal)coupon.PercentDiscount/100));
                orderQuery.CouponId = coupon.Id;
                orderQuery.UpdateDate = tranDateTime;
                orderQuery.UpdateBy = "system";

                this.dbContext.Set<Order>().Update(orderQuery);
                dbContext.SaveChanges();

                response.Item = mapper.Map<OrderDTO>(orderQuery);

                response.Item.TotalAmount = response.Item.TotalAmount * (1 - ((decimal)coupon.PercentDiscount / 100));

                response.ErrorCode = "0000";
                response.ErrorMessage = "Success.";

            }
            catch (Exception ex)
            {
                response.ErrorCode = "BE9999";
                response.ErrorMessage = "Internal server error.";
            }

            return response;
        }
    }
}
