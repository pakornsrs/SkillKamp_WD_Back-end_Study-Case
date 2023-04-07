﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.DTO.Enums;
using Web.Backend.DTO;
using Web.Backend.DTO.Orders;

namespace Web.Backend.BLL.IServices
{
    public interface IDiscountCouponService
    {
        public ServiceResponseModel<DefaultResponseModel> GenerateDiscountCoupon(int userId, DiscountCouponType type, decimal percenDiscount, int limitation);
        public ServiceResponseModel<OrderDTO> ApplyDiscountCoupon(int userId, int orderId, string couponCode);
        public ServiceResponseModel<DefaultResponseModel> UpdateStatusDiscountCoupon(int couponId);
    }
}
