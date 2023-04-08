using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.BLL.IServices;
using Web.Backend.BLL.ModelMappers;
using Web.Backend.BLL.UtilityMethods;
using Web.Backend.DAL;
using Web.Backend.DAL.Entities;
using Web.Backend.DTO;
using Web.Backend.DTO.Coupon;
using Web.Backend.DTO.Enums;
using Web.Backend.DTO.Orders;
using static Azure.Core.HttpHeader;

namespace Web.Backend.BLL.Services
{
    public class DiscountCouponService : IDiscountCouponService
    {
        private readonly SkillkampWdStudyCaseDbContext dbContext;
        private readonly IPurchaseSessionService purchaseSessionService;
        private readonly ICartItemService cartItemService;
        private readonly IOrderService orderService;

        private IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile<ModelMapper>()).CreateMapper();

        public DiscountCouponService(SkillkampWdStudyCaseDbContext dbContext,
                                     IPurchaseSessionService purchaseSessionService,
                                     ICartItemService cartItemService,
                                     IOrderService orderService)
        {
            this.dbContext = dbContext;
            this.purchaseSessionService = purchaseSessionService;
            this.cartItemService = cartItemService;
            this.orderService = orderService;
        }

        public ServiceResponseModel<DefaultResponseModel> GenerateDiscountCoupon(int userId, DiscountCouponType type, decimal percenDiscount, int limitation)
        {
            var response = new ServiceResponseModel<DefaultResponseModel>();
            var defaultModel = new DefaultResponseModel();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            using (IDbContextTransaction transaction = this.dbContext.Database.BeginTransaction())
            {
                try
                {
                    string coupon = "";

                    while (true)
                    {
                        coupon = GenerateCouponNumber();

                        //check available

                        var couponQuery = (from q in this.dbContext.DiscountCoupons
                                           where q.CouponCode == coupon
                                           select q).FirstOrDefault();

                        if (couponQuery == null) break;
                    }

                    var discountCoupon = new DiscountCoupon();

                    discountCoupon.UserId = userId;
                    discountCoupon.Type = (int) type;
                    discountCoupon.CouponCode = coupon;
                    discountCoupon.IsActive = true;
                    discountCoupon.Limitation = limitation;
                    discountCoupon.PercentDiscount = percenDiscount;
                    discountCoupon.UseCount = 0;
                    discountCoupon.CreateDate = tranDateTime;
                    discountCoupon.ExpireDate = tranDateTime.AddDays(30);
                    discountCoupon.UseDate = tranDateTime;
                    discountCoupon.UpdateDate = tranDateTime;
                    discountCoupon.CreateBy = "system";
                    discountCoupon.UpdateBy = "system";

                    dbContext.Set<DiscountCoupon>().Add(discountCoupon);
                    dbContext.SaveChanges();

                    defaultModel.message = "Add coupon for user success";

                    response.Item = defaultModel;

                    response.ErrorCode = "0000";
                    response.ErrorMessage = "Success";

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    response.ErrorCode = "BE9999";
                    response.ErrorMessage = "Internal server error.";
                }
            }

            return response;
        }

        public ServiceResponseModel<OrderDTO> ApplyDiscountCoupon(int userId, int orderId, string couponCode)
        {
            var response = new ServiceResponseModel<OrderDTO>();
            var defaultModel = new DefaultResponseModel();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            using (IDbContextTransaction transaction = this.dbContext.Database.BeginTransaction())
            {
                try
                {
                    // Check coupon

                    var couponQuery = (from q in this.dbContext.DiscountCoupons
                                       where q.CouponCode == couponCode && q.IsActive == true
                                       select q).FirstOrDefault();

                    if(couponQuery == null)
                    {
                        response.ErrorCode = "AD0001";
                        response.ErrorMessage = "Conpon not found.";

                        return response;
                    }

                    // Check user Id

                    if (couponQuery.Type == (int)DiscountCouponType.SpecificUser)
                    {
                        if(couponQuery.UserId != 0 && couponQuery.UserId != userId)
                        {
                            response.ErrorCode = "AD0002";
                            response.ErrorMessage = "This coupon is not valid for you";

                            return response;
                        }
                    }

                    // Check limitation

                    if(couponQuery.UseCount >= couponQuery.Limitation)
                    {
                        response.ErrorCode = "AD0003";
                        response.ErrorMessage = "This coupon is reached the limit";

                        return response;
                    }

                    // Check expire data

                    if (couponQuery.ExpireDate < tranDateTime)
                    {
                        response.ErrorCode = "AD0004";
                        response.ErrorMessage = "This coupon is expired.";

                        return response;
                    }

                    // Update session

                    var coupon = mapper.Map<DiscountCouponDTO>(couponQuery);

                    var orderServiceResponse = orderService.ApplyDiscountCoupon(orderId, coupon);

                    if (orderServiceResponse.IsError)
                    {
                        response.ErrorCode = "AD0005";
                        response.ErrorMessage = "Cannot applied coupon.";

                        return response;
                    }

                    response.Item = orderServiceResponse.Item;

                    response.ErrorCode = "0000";
                    response.ErrorMessage = "Success";

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    response.ErrorCode = "BE9999";
                    response.ErrorMessage = "Internal server error.";
                }
            }

            return response;
        }

        public ServiceResponseModel<DefaultResponseModel> UpdateStatusDiscountCoupon(int couponId)
        {
            var response = new ServiceResponseModel<DefaultResponseModel>();
            var defaultModel = new DefaultResponseModel();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            try
            {
                var couponQuery = (from q in dbContext.DiscountCoupons
                                   where q.Id == couponId && q.IsActive == true && q.UseCount < q.Limitation
                                   select q).First();

                if(couponQuery == null)
                {
                    response.ErrorCode = "AD0005";
                    response.ErrorMessage = "Coupon not found or out of limit.";
                }

                couponQuery.UseCount = couponQuery.UseCount + 1;

                if(couponQuery.UseCount >= couponQuery.Limitation) couponQuery.IsActive = false;

                couponQuery.UseDate = tranDateTime;
                couponQuery.UpdateDate = tranDateTime;
                couponQuery.UpdateBy = "system";

                defaultModel.message = "Update status coupon success";
                response.Item = defaultModel;

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



        private string GenerateCouponNumber()
        {
            return "Test";
        }
    }
}
