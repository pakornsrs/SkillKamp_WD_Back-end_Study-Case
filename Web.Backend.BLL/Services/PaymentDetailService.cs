using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.BLL.IServices;
using Web.Backend.BLL.ModelMappers;
using Web.Backend.BLL.UtilityMethods;
using Web.Backend.DAL;
using Web.Backend.DTO.Enums;
using Web.Backend.DTO;
using Web.Backend.DTO.PaymentDetail;
using Web.Backend.DAL.Entities;

namespace Web.Backend.BLL.Services
{
    public class PaymentDetailService : IPaymentDetailService
    {
        private readonly SkillkampWdStudyCaseDbContext dbContext;

        private IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile<ModelMapper>()).CreateMapper();

        public PaymentDetailService(SkillkampWdStudyCaseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ServiceResponseModel<PaymentDetailDTO> InsertPaymentDetail(int userId, int orderId, int paymentMethod, int status, int addressId, string? addressDetail, int? cardId = 0)
        {
            var response = new ServiceResponseModel<PaymentDetailDTO>();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            try
            {
                var detail = new PaymentDetail();

                detail.UserId = userId;
                detail.OrderId = orderId;
                detail.PaymentMethod = paymentMethod;
                detail.Status = status;
                detail.CardId = cardId;
                detail.DeliveryAddressId = addressId;
                detail.AddressDetail = addressDetail;
                detail.CreateDate = tranDateTime;
                detail.UpdateDate = tranDateTime;
                detail.CreateBy = "system";
                detail.UpdateBy = "system";

                dbContext.Set<PaymentDetail>().Add(detail);
                dbContext.SaveChanges();

                var result = mapper.Map<PaymentDetailDTO>(detail);

                response.Item = result;
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
