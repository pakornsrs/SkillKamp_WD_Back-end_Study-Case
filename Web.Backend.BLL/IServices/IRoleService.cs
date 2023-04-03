using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.DTO;
using Web.Backend.DTO.Roles;

namespace Web.Backend.BLL.IServices
{
    public interface IRoleService
    {
        public ServiceResponseModel<List<RoleDTO>> GetRoles();
    }
}
