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
using Web.Backend.Models.DiscountCoupon;
using Web.Backend.Models.Order;

namespace Web.Backend.Test.Controllers
{
    public class OrderControllerTest
    {
        private readonly IOrderService orderService;
        private readonly IPurchasedOrderService purchasedOrderService;

        public OrderControllerTest()
        {
            orderService = A.Fake<IOrderService>();
            purchasedOrderService = A.Fake<IPurchasedOrderService>();
        }

        [Fact]
        public void CreateProductOrder_Response_Is_NotNull()
        {
            var Request = A.Fake<UserIdRequestModel>();
            var controller = new OrderController(orderService, purchasedOrderService);

            var result = controller.CreateProductOrder(Request);

            Assert.NotNull(result);
        }

        [Fact]
        public void PurchaseOrder_Response_Is_NotNull()
        {
            var Request = A.Fake<PurchaseOrderRequestModel>();
            var controller = new OrderController(orderService, purchasedOrderService);

            var result = controller.PurchaseOrder(Request);

            Assert.NotNull(result);
        }

    }
}
