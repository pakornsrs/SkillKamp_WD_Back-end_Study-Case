using Microsoft.AspNetCore.Mvc;
using Web.Backend.BLL.IServices;
using Web.Backend.BLL.Services;
using Web.Backend.DTO.Users;
using Web.Backend.DTO;
using Web.Backend.Models.Campeigns;
using Web.Backend.Models.CartItems;
using Web.Backend.DTO.CartItem;
using Web.Backend.Models;
using Microsoft.AspNetCore.Authorization;

namespace Web.Backend.Controllers
{
    [Authorize]
    public class CartItemController : Controller
    {
        private readonly ICartItemService cartItemService;
        public CartItemController(ICartItemService cartItemService)
        {
            this.cartItemService = cartItemService;
        }

        /// <summary>
        /// (To add item to cart)
        /// </summary>
        /// <remarks>
        /// Detail
        /// 
        ///    This api use when user select some product and add to the cart.
        ///     
        /// </remarks>
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


        /// <summary>
        /// (To add number of item in cart)
        /// </summary>
        /// <remarks>
        /// Detail
        /// 
        ///    This api use when user add more number of item in cart.
        ///    However, user cannot add item more than stock quantity.
        ///     
        /// </remarks>
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


        /// <summary>
        /// (To reduct number of item in cart)
        /// </summary>
        /// <remarks>
        /// Detail
        /// 
        ///    This api use when user add more number of item in cart.
        ///    However, user cannot reduce item lower than 0.
        ///     
        /// </remarks>
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

        /// <summary>
        /// (To remove item in cart)
        /// </summary>
        /// <remarks>
        /// Detail
        /// 
        ///    This api use when user click remove button then item will be removed from cart.
        ///     
        /// </remarks>
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

        /// <summary>
        /// (To get cart item count)
        /// </summary>
        /// <remarks>
        /// Detail
        /// 
        ///    This api use for updating the number that show on cart icon.
        ///     
        /// </remarks>
        [HttpPost()]
        [Route("api/card/item/count")]
        public async Task<IActionResult> GetUserCartItemCount([FromBody] UserIdRequestModel req)
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

        /// <summary>
        /// (To get all cart item)
        /// </summary>
        /// <remarks>
        /// Detail
        /// 
        ///    This api use when user click cart icon. it will recieve prodeuct detail for displaying on cart modal.
        ///     
        /// </remarks>
        [HttpPost()]
        [Route("api/card/item/get/all")]
        public async Task<IActionResult> GetAllCartProduct([FromBody] UserIdRequestModel req)
        {
            var result = new ServiceResponseModel<List<ProductCartItemDTO>>();

            try
            {
                result = cartItemService.GetAllCartProduct(req.userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return StatusCode(200, result);
        }
    }
}
