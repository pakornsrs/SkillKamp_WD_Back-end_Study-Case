using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.BLL.IServices;
using Web.Backend.BLL.Services;
using Web.Backend.Controllers;
using Web.Backend.Models;
using Web.Backend.Models.CartItems;

namespace Web.Backend.Test.Controllers
{
    public class CartItemControllerTest
    {
        private readonly ICartItemService cartItemService;
        public CartItemControllerTest()
        {
            cartItemService = A.Fake<ICartItemService>();
        }

        [Fact]
        public void AddNewItemToCart_IsNotNull()
        {
            var request = A.Fake<AddItmeRequestModel>();

            var controller = new CartItemController(cartItemService);
            var result = controller.AddNewItemToCart(request);

            Assert.NotNull(result);
        }

        [Fact]
        public void AddItemQuantityToCart_IsNotNull()
        {
            var request = A.Fake<AddOrReduceRequestModel>();

            var controller = new CartItemController(cartItemService);
            var result = controller.AddItemQuantityToCart(request);

            Assert.NotNull(result);
        }

        [Fact]
        public void ReduceItemQuantityInCart_IsNotNull()
        {
            var request = A.Fake<AddOrReduceRequestModel>();

            var controller = new CartItemController(cartItemService);
            var result = controller.ReduceItemQuantityInCart(request);

            Assert.NotNull(result);
        }

        [Fact]
        public void RemoveItemInCart_IsNotNull()
        {
            var request = A.Fake<CartItemIdRequestModel>();

            var controller = new CartItemController(cartItemService);
            var result = controller.RemoveItemInCart(request);

            Assert.NotNull(result);
        }

        [Fact]
        public void DeleteCartSession_IsNotNull()
        {
            var request = A.Fake<PurchaseSessionIdRequestModel>();

            var controller = new CartItemController(cartItemService);
            var result = controller.DeleteCartSession(request);

            Assert.NotNull(result);
        }

        [Fact]
        public void GetUserCartItemCount_IsNotNull()
        {
            var request = A.Fake<UserIdRequestModel>();

            var controller = new CartItemController(cartItemService);
            var result = controller.GetUserCartItemCount(request);

            Assert.NotNull(result);
        }

        [Fact]
        public void GetAllCartProduct_IsNotNull()
        {
            var request = A.Fake<UserIdRequestModel>();

            var controller = new CartItemController(cartItemService);
            var result = controller.GetAllCartProduct(request);

            Assert.NotNull(result);
        }


    }
}
