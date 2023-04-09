using Microsoft.AspNetCore.Mvc;
using Web.Backend.BLL.IServices;
using Web.Backend.DTO.Users;
using Web.Backend.DTO;
using Web.Backend.Models.Users;
using Web.Backend.Models.Products;
using Web.Backend.DTO.Products;
using Web.Backend.Models;

namespace Web.Backend.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly IProductDetailService productDetailService;
        public ProductController(IProductService productService, 
                                 IProductDetailService productDetailService)
        {
            this.productService = productService;
            this.productDetailService = productDetailService;
        }

        [HttpPost()]
        [Route("api/product/add")]
        public async Task<IActionResult> AddNewProduct([FromBody] AddProductRequestModel req)
        {
            var result = new ServiceResponseModel<DefaultResponseModel>();

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
        [Route("api/product/new/detail")]
        public async Task<IActionResult> AddNewProductDetail([FromBody] AddNewProductDetailRequestModel req)
        {
            var result = new ServiceResponseModel<DefaultResponseModel>();

            try
            {
                result = productDetailService.AddAditionalDetail(req.ProductDetails);
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
            var result = new ServiceResponseModel<List<ProductSearchResultDTO>>();

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

        [HttpGet()]
        [Route("api/product/new")]
        public async Task<IActionResult> NewArrivalProd()
        {
            var result = new ServiceResponseModel<List<ProductSearchResultDTO>>();

            try
            {
                result = productService.GetNewArrival();
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }

        [HttpPost()]
        [Route("api/product/detail/get")]
        public async Task<IActionResult> GetProductDetail([FromBody] ProductIdRequestModel req)
        {
            var result = new ServiceResponseModel<ProductSearchResultDTO>();

            try
            { 
                result = productService.GetProductFullDetail(req.productId);
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }
    }
}
