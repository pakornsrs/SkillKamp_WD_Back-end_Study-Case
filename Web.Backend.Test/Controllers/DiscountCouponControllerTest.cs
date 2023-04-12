using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.BLL.IServices;
using Web.Backend.BLL.Services;
using Web.Backend.Controllers;
using Web.Backend.Models.Campeigns;
using Web.Backend.Models.DiscountCoupon;

namespace Web.Backend.Test.Controllers
{
    public class DiscountCouponControllerTest
    {
        private readonly IDiscountCouponService discountCouponService;
        public DiscountCouponControllerTest()
        {
            discountCouponService = A.Fake<IDiscountCouponService>();
        }

        [Fact]
        public void GenerateDiscountCoupon_Response_Is_NotNull()
        {
            var Request = A.Fake<GeneratCouponRequestModel>();
            var controller = new DiscountCouponController(discountCouponService);

            var result = controller.GenerateDiscountCoupon(Request);

            Assert.NotNull(result);
        }

        [Fact]
        public void ApplyDiscountCoupon_Response_Is_NotNull()
        {
            var Request = A.Fake<ApplyCouponRequestModel>();
            var controller = new DiscountCouponController(discountCouponService);

            var result = controller.ApplyDiscountCoupon(Request);

            Assert.NotNull(result);
        }
    }
}
