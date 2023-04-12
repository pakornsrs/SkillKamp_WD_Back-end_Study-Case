using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.BLL.IServices;
using Web.Backend.Controllers;
using Web.Backend.DTO.Enums;
using Web.Backend.Models;
using Web.Backend.Models.Users;
using ActionResult = Web.Backend.DTO.Enums.ActionResult;

namespace Web.Backend.Test.Controllers
{
    public class CardControllerTest
    {
        private readonly IUserCardService userCardService;
        public CardControllerTest()
        {
            userCardService = A.Fake<IUserCardService>();
        }

        [Fact]
        public void GetUserCardById_IsNotNull()
        {
            var request = A.Fake<UserIdRequestModel>();

            var controller = new CardController(userCardService);

            Assert.NotNull(controller.GetCardByUSerId(request));
        }

        [Fact]
        public void AddUserCardResponse_IsNotNull()
        {
            var request = A.Fake<AddCardRequestModel>();

            var controller = new CardController(userCardService);

            Assert.NotNull(controller.AddUserCard(request));
        }

    }
}
