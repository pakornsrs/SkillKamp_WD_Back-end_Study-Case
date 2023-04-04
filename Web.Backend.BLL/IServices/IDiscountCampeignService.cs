using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.DTO.Campeigns;
using Web.Backend.DTO;

namespace Web.Backend.BLL.IServices
{
    public interface IDiscountCampeignService
    {
        public ServiceResponseModel<CampeignsDTO> AddDiscountCampeign(CampeignsDTO campeignReq);
        public ServiceResponseModel<CampeignsDTO> UpdateDiscountCampeign(CampeignsDTO campeignReq);
        public ServiceResponseModel<string> TerminateDiscountCampeign(List<int> campeignIdList);
    }
}
