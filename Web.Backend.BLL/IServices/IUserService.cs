using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.DTO.Addresses;
using Web.Backend.DTO.Cards;
using Web.Backend.DTO.Users;
using Web.Backend.DTO;
using Web.Backend.DTO.Enums;

namespace Web.Backend.BLL.IServices
{
    public interface IUserService
    {
        public ServiceResponseModel<RegistrationDTO> Registration(UserDTO user, List<AddressDTO> addressList, List<CardRequestDTO> userCard);
        public  ServiceResponseModel<LoginDTO> Login(string username, string password);
    }
}
