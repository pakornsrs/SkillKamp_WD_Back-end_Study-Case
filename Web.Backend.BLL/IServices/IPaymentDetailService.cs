using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.DTO.PaymentDetail;
using Web.Backend.DTO;

namespace Web.Backend.BLL.IServices
{
    public interface IPaymentDetailService
    {
        public ServiceResponseModel<PaymentDetailDTO> InsertPaymentDetail(int userId, int orderId, int paymentMethod, int status, int addressId, string? addressDetail, int? cardId = 0);
    }
}
