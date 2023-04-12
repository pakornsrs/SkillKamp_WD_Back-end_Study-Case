using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.BLL.IServices;
using Web.Backend.BLL.Services;
using Web.Backend.Controllers;
using Web.Backend.Models;
using Web.Backend.Models.Campeigns;

namespace Web.Backend.Test.Controllers
{
    public class DiscountCampeignControllerTest
    {
        private readonly IDiscountCampeignService discountCampeignService;
        public DiscountCampeignControllerTest()
        {
            discountCampeignService = A.Fake<IDiscountCampeignService>();
        }

        [Fact]
        public void AddDiscountCampeign_Response_Is_NotNull()
        {
            var Request = A.Fake<AddOrUpdateCampeignRequestModel>();
            var controller = new DiscountCampeignController(discountCampeignService);

            var result = controller.AddDiscountCampeign(Request);

            Assert.NotNull(result);
        }

        [Fact]
        public void UpdateDiscountCampeign_Response_Is_NotNull()
        {
            var Request = A.Fake<AddOrUpdateCampeignRequestModel>();
            var controller = new DiscountCampeignController(discountCampeignService);

            var result = controller.UpdateDiscountCampeign(Request);

            Assert.NotNull(result);
        }

        [Fact]
        public void TerminateDiscountCampeign_Response_Is_NotNull()
        {
            var Request = A.Fake<TerminateCampeignRequestModel>();
            var controller = new DiscountCampeignController(discountCampeignService);

            var result = controller.TerminateDiscountCampeign(Request);

            Assert.NotNull(result);
        }

    }
}
