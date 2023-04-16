using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.DTO;

namespace Web.Backend.BLL.IServices
{
    public interface IPurchasedOrderService
    {
        public ServiceResponseModel<DefaultResponseModel> PurchastOrder(int userId, int orderId, int paymentType, int cardId, int? couponId);
    }
}
