using Microsoft.AspNetCore.Mvc;
using Web.Backend.BLL.IServices;
using Web.Backend.BLL.Services;
using Web.Backend.DTO.Orders;
using Web.Backend.DTO;
using Web.Backend.Models;
using Web.Backend.Models.DiscountCoupon;
using Web.Backend.DTO.Enums;
using Web.Backend.DTO.Coupon;
using Microsoft.AspNetCore.Authorization;

namespace Web.Backend.Controllers
{
    [Authorize]
    public class DiscountCouponController : Controller
    {
        private readonly IDiscountCouponService discountCouponService;

        public DiscountCouponController(IDiscountCouponService discountCouponService)
        {
            this.discountCouponService = discountCouponService;
        }

        /// <summary>
        /// (To create discount coupon
        /// </summary>
        /// <remarks>
        /// Detail
        /// 
        ///    This api use to cheate discount coupon.
        ///    The type of counpon defined by 0 = coupon for all user, 1  = coupon for specific user Id.
        /////    However, this service currently have no UI so you can try this api using Postman.
        ///     
        /// </remarks>
        [HttpPost()]
        [Route("api/coupon/create")]
        public async Task<IActionResult> GenerateDiscountCoupon([FromBody] GeneratCouponRequestModel req)
        {
            var result = new ServiceResponseModel<DefaultResponseModel>();

            try
            {
                result = discountCouponService.GenerateDiscountCoupon(req.UserId,(DiscountCouponType) req.Type, req.PercenDiscount, req.Limitation);
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }

        /// <summary>
        /// (To use discount coupon)
        /// </summary>
        /// <remarks>
        /// Detail
        /// 
        ///    This api use to check coupon and calculate the total price after discount.
        ///     
        /// </remarks>
        [HttpPost()]
        [Route("api/coupon/apply")]
        public async Task<IActionResult> ApplyDiscountCoupon([FromBody] ApplyCouponRequestModel req)
        {
            var result = new ServiceResponseModel<OrderDTO>();

            try
            {
                result = discountCouponService.ApplyDiscountCoupon(req.UserId, req.OrderId, req.CouponCode);
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }

        /// <summary>
        /// (To get all coupon that user can use)
        /// </summary>
        /// <remarks>
        /// Detail
        /// 
        ///    This api use to get all coupon that user can use.
        ///     
        /// </remarks>
        [HttpPost()]
        [Route("api/coupon/get")]
        public async Task<IActionResult> GetUserCoupon([FromBody] UserIdRequestModel req)
        {
            var result = new ServiceResponseModel<List<DiscountCouponDTO>>();

            try
            {
                result = discountCouponService.GetUserCoupon(req.userId);
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }


    }
}
