using Microsoft.AspNetCore.Mvc;
using Web.Backend.BLL.IServices;
using Web.Backend.DTO.Users;
using Web.Backend.DTO;
using Web.Backend.Models.Users;
using Web.Backend.Models.Products;
using Web.Backend.DTO.Products;

namespace Web.Backend.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpPost()]
        [Route("api/product/add")]
        public async Task<IActionResult> AddNewProduct([FromBody] AddProductRequestModel req)
        {
            var result = new ServiceResponseModel<ProductDTO>();

            try
            {
                result = productService.AddNewProduct(req.AddProduct, req.ProductSpecificDetail, req.Inventory);
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }

        [HttpPost()]
        [Route("api/product/search")]
        public async Task<IActionResult> SearchProduct([FromBody] SearchProductRequestMode req)
        {
            var result = new ServiceResponseModel<List<ProductDTO>>();

            try
            {
                result = productService.SerchProductByKeyword(req.Keywork);
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }
    }
}
