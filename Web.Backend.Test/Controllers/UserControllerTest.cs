using FakeItEasy;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.BLL.IServices;
using Web.Backend.BLL.Services;
using Web.Backend.Controllers;
using Web.Backend.Jwt;
using Web.Backend.Models.Products;
using Web.Backend.Models.Users;

namespace Web.Backend.Test.Controllers
{
    public class UserControllerTest
    {
        private readonly IUserService userService;
        public UserControllerTest()
        {
            userService = A.Fake<IUserService>();
        }

        [Fact]
        public void Registration_Response_Is_NotNull()
        {
            var Request = A.Fake<RegistrationRequestModel>();
            var controller = new UserController(userService,null);

            var result = controller.Registration(Request);

            Assert.NotNull(result);
        }

        [Fact]
        public void Login_Response_Is_NotNull()
        {
            var Request = A.Fake<LoginRequestModel>();
            var controller = new UserController(userService, null);

            var result = controller.Login(Request);

            Assert.NotNull(result);
        }
    }
}
