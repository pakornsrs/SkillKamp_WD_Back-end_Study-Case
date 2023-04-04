using Web.Backend.DTO.ProductDetails;
using Web.Backend.DTO.Products;

namespace Web.Backend.Models.Products
{
    public class AddNewProductDetailRequestModel
    {
        public List<ProductDetailDTO> ProductDetails { get; set; }   
    }
}
