using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.DTO.Addresses;
using Web.Backend.DTO.Cards;
using Web.Backend.DTO.Users;
using Web.Backend.DTO;

namespace Web.Backend.BLL.IServices
{
    public interface IUserService
    {
        public Task<ServiceResponseModel<RegistrationDTO>> Registration(UserDTO user, List<AddressDTO> addressList, List<CardDTO> userCard);
        public  Task<ServiceResponseModel<LoginDTO>> Login(string username, string password);
    }
}
