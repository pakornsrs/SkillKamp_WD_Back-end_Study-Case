using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.DTO.Orders;
using Web.Backend.DTO;
using Web.Backend.DTO.Coupon;
using Web.Backend.DTO.Enums;

namespace Web.Backend.BLL.IServices
{
    public interface IOrderService
    {
        public ServiceResponseModel<OrderDTO> CreateProductOrder(int userId);
        public ServiceResponseModel<OrderDTO> GetOrderDetail(int orderId);
        public ServiceResponseModel<List<OrderDTO>> GetOrderDetailList(List<int?> orderIds);
        public ServiceResponseModel<bool> CancelOrder(int orderId);
        public ServiceResponseModel<OrderDTO> ApplyDiscountCoupon(int orderId, DiscountCouponDTO coupon);

        public ServiceResponseModel<OrderDTO> UpdateOrderStatus(int orderId, OrderStatus status);
    }
}
