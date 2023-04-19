using Microsoft.AspNetCore.Mvc;
using Web.Backend.BLL.IServices;
using Web.Backend.DTO.Cards;
using Web.Backend.DTO;
using Web.Backend.Models.Users;
using Web.Backend.Models;
using Web.Backend.DTO.Addresses;
using Microsoft.AspNetCore.Authorization;

namespace Web.Backend.Controllers
{
    [Authorize]
    public class AddressController : Controller
    {
        private readonly IUserAddressService userAddressServicce;
        public AddressController(IUserAddressService userAddressServicce)
        {
            this.userAddressServicce = userAddressServicce;
        }

        /// <summary>
        /// (To get user registed address)
        /// </summary>
        /// <remarks>
        /// Detail
        /// 
        ///     To get the addresses that user gives on the registration process. 
        ///     The addresses will be displayed on product summary page then user can select them for deliver address.
        ///     
        /// </remarks>
        [HttpPost()]
        [Route("api/address/get")]
        public async Task<IActionResult> GetAddressByUserId([FromBody] UserIdRequestModel req)
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