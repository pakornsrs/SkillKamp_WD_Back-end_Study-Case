using Web.Backend.DTO.Inventories;
using Web.Backend.DTO.ProductDetails;
using Web.Backend.DTO.Products;

namespace Web.Backend.Models.Products
{
    public class AddProductRequestModel
    {
        public AddProductDTO AddProduct { get; set; }
        public List<InventoryDTO> Inventory { get; set; }
        public List<ProductDetailDTO> ProductSpecificDetail { get; set; }
    }
}
