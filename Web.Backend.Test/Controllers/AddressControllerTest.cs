using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.BLL.IServices;
using Web.Backend.DTO.Addresses;
using Web.Backend.DTO;
using Web.Backend.Models;
using Web.Backend.Controllers;

namespace Web.Backend.Test.Controllers
{
    public class AddressControllerTest
    {
        private readonly IUserAddressService userAddressServicce;
        public AddressControllerTest()
        {
            userAddressServicce = A.Fake<IUserAddressService>();
        }

        [Fact]
        public void AddressController_Response_Is_NotNull()
        {
            var Request = A.Fake<UserIdRequestModel>();
            var controller = new AddressController(userAddressServicce);

            var result = controller.GetAddressByUserId(Request);

            Assert.NotNull(result);
        }

    }
}
