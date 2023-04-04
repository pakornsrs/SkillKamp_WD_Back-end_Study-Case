using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.DTO;
using Web.Backend.DTO.CartItem;

namespace Web.Backend.BLL.IServices
{
    public interface ICartItemService
    {
        public ServiceResponseModel<CartItemDTO> AddNewItemToCart(int userId, int productId, int productDetail, int quantity);
        public ServiceResponseModel<CartItemDTO> AddItemQuantityToCart(int cartItemId, int quantity);
        public ServiceResponseModel<CartItemDTO> ReduceItemQuantityInCart(int cartItemId, int quantity);
        public ServiceResponseModel<DefaultResponseModel> RemoveItemInCart(int cartItemId);
        public ServiceResponseModel<DefaultResponseModel> DeleteCartSession(int purchaseSession);
    }
}
