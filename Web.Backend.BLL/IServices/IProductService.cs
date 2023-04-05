using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.DTO.Inventories;
using Web.Backend.DTO.ProductDetails;
using Web.Backend.DTO.Products;
using Web.Backend.DTO;

namespace Web.Backend.BLL.IServices
{
    public interface IProductService
    {
        public ServiceResponseModel<DefaultResponseModel> AddNewProduct(AddProductDTO productReq, List<ProductDetailDTO> productDetailReq, List<InventoryDTO> inventoryReq);
        public ServiceResponseModel<List<ProductSearchResultDTO>> SerchProductByKeyword(string Keyword);
        public ServiceResponseModel<int?> GetAllProductCount();

    }
}
