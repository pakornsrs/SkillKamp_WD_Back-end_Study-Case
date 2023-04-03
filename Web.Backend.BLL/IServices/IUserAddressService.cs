using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.DTO;
using Web.Backend.DTO.Addresses;
using Web.Backend.DTO.Enums;

namespace Web.Backend.BLL.IServices
{
    public interface IUserAddressService
    {
        public ServiceResponseModel<List<AddressDTO>> GetAddressByUserId(int userId);
        public ServiceResponseModel<ActionResult> AddUserAddresses(int userId, List<AddressDTO> addressList);
    }
}
