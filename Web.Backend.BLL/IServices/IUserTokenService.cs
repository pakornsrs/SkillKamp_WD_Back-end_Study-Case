using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.DTO;
using Web.Backend.DTO.UserTokens;

namespace Web.Backend.BLL.IServices
{
    public interface IUserTokenService
    {
        public ServiceResponseModel<UserTokenDTO> CreateUserToken(int userId, string username, string role);
        public ServiceResponseModel<UserTokenDTO> UpdateUserToken(int tokenId);
    }
}
