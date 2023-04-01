using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.DTO.Config;
using Web.Backend.DTO;

namespace Web.Backend.BLL.IServices
{
    public interface ISystemConfigService
    {
        public ServiceResponseModel<GetServiceVersionDTO> GetVersion();
    }
}
