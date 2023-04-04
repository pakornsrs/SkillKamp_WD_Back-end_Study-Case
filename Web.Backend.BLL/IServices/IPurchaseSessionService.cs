using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.DTO.PurchastSession;
using Web.Backend.DTO;

namespace Web.Backend.BLL.IServices
{
    public interface IPurchaseSessionService
    {
        public ServiceResponseModel<PurchaseSessionDTO> CheckActiveSession(int userId);
        public ServiceResponseModel<PurchaseSessionDTO> CreateNewSession(int userId);
        public ServiceResponseModel<DefaultResponseModel> TerminatePurchastSession(int purchastId);
    }
}
