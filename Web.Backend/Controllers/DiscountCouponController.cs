﻿using Microsoft.AspNetCore.Mvc;
using Web.Backend.BLL.IServices;
using Web.Backend.BLL.Services;
using Web.Backend.DTO.Orders;
using Web.Backend.DTO;
using Web.Backend.Models;
using Web.Backend.Models.DiscountCoupon;
using Web.Backend.DTO.Enums;

namespace Web.Backend.Controllers
{
    public class DiscountCouponController : Controller
    {
        private readonly IDiscountCouponService discountCouponService;

        public DiscountCouponController(IDiscountCouponService discountCouponService)
        {
            this.discountCouponService = discountCouponService;
        }

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


    }
}