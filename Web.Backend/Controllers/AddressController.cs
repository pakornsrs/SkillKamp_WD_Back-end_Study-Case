using Microsoft.AspNetCore.Mvc;
using Web.Backend.BLL.IServices;
using Web.Backend.DTO.Cards;
using Web.Backend.DTO;
using Web.Backend.Models.Users;
using Web.Backend.Models;
using Web.Backend.DTO.Addresses;

namespace Web.Backend.Controllers
{
    public class AddressController : Controller
    {
        private readonly IUserAddressService userAddressServicce;
        public AddressController(IUserAddressService userAddressServicce)
        {
            this.userAddressServicce = userAddressServicce;
        }

        [HttpPost()]
        [Route("api/address/get")]
        public async Task<IActionResult> GetCardByUSerId([FromBody] UserIdRequestModel req)
        {
            var result = new ServiceResponseModel<List<AddressDTO>>();

            try
            {
                result = userAddressServicce.GetAddressByUserId(req.userId);
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }


    }
}