using Microsoft.AspNetCore.Mvc;
using Web.Backend.BLL.IServices;
using Web.Backend.BLL.Services;
using Web.Backend.DTO.Users;
using Web.Backend.DTO;
using Web.Backend.Models.Campeigns;
using Web.Backend.Models.CartItems;
using Web.Backend.DTO.CartItem;
using Web.Backend.Models;

namespace Web.Backend.Controllers
{
    public class CartItemController : Controller
    {
        private readonly ICartItemService cartItemService;
        public CartItemController(ICartItemService cartItemService)
        {
            this.cartItemService = cartItemService;
        }

        [HttpPost()]
        [Route("api/cart/add/new")]
        public async Task<IActionResult> AddNewItemToCart([FromBody] AddItmeRequestModel req)
        {
            var result = new ServiceResponseModel<CartItemDTO>();

            try
            {
                result = cartItemService.AddNewItemToCart(req.userId, req.productId, req.productDetail, req.quantity,req.price);
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }

        [HttpPost()]
        [Route("api/cart/add/quantity")]
        public async Task<IActionResult> AddItemQuantityToCart([FromBody] AddOrReduceRequestModel req)
        {
            var result = new ServiceResponseModel<CartItemDTO>();

            try
            {
                result = cartItemService.AddItemQuantityToCart(req.CartItemId,req.Quantiry);
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }

        [HttpPost()]
        [Route("api/cart/reduce/quantit")]
        public async Task<IActionResult> ReduceItemQuantityInCart([FromBody] AddOrReduceRequestModel req)
        {
            var result = new ServiceResponseModel<CartItemDTO>();

            try
            {
                result = cartItemService.ReduceItemQuantityInCart(req.CartItemId, req.Quantiry);
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }

        [HttpPost()]
        [Route("api/cart/remove")]
        public async Task<IActionResult> RemoveItemInCart([FromBody] CartItemIdRequestModel req)
        {
            var result = new ServiceResponseModel<DefaultResponseModel>();

            try
            {
                result = cartItemService.RemoveItemInCart(req.CartItemId);
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }

        [HttpPost()]
        [Route("api/cart/delete/all")]
        public async Task<IActionResult> DeleteCartSession([FromBody] PurchaseSessionIdRequestModel req)
        {
            var result = new ServiceResponseModel<DefaultResponseModel>();

            try
            {
                result = cartItemService.DeleteCartSession(req.PurchaseSessionId);
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }

        [HttpPost()]
        [Route("api/card/item/count")]
        public async Task<IActionResult> GetCardItemCount([FromBody] UserIdRequestModel req)
        {
            var result = new ServiceResponseModel<int>();

            try
            {
                result = cartItemService.GetUserCartItemCount(req.userId);
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }

        [HttpPost()]
        [Route("api/card/item/get/all")]
        public async Task<IActionResult> GetAllCardItem([FromBody] UserIdRequestModel req)
        {
            var result = new ServiceResponseModel<List<ProductCartItemDTO>>();

            try
            {
                result = cartItemService.GetAllCartProduct(req.userId);
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }
    }
}
