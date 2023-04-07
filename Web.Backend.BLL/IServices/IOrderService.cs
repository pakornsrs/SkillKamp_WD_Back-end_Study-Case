using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.DTO.Orders;
using Web.Backend.DTO;
using Web.Backend.DTO.Coupon;

namespace Web.Backend.BLL.IServices
{
    public interface IOrderService
    {
        public ServiceResponseModel<OrderDTO> CreateProductOrder(int sessionId);
        public ServiceResponseModel<OrderDTO> ApplyDiscountCoupon(int orderId, DiscountCouponDTO coupon);
    }
}
