using Microsoft.AspNetCore.Mvc;
using Web.Backend.BLL.IServices;
using Web.Backend.DTO.Users;
using Web.Backend.DTO;
using Web.Backend.Models.Users;
using Web.Backend.Models.Products;
using Web.Backend.DTO.Products;
using Web.Backend.Models;
using Web.Backend.DTO.ProductSizes;
using Web.Backend.DTO.ProductColors;
using Web.Backend.DTO.Category;

namespace Web.Backend.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly IProductDetailService productDetailService;
        private readonly IProductSizeService productSizeService;
        private readonly IProductColorService productColorService;
        private readonly ICategoryService categoryService;
        public ProductController(IProductService productService, 
                                 IProductDetailService productDetailService,
                                 IProductSizeService productSizeService,
                                 IProductColorService productColorService,
                                 ICategoryService categoryService)
        {
            this.productService = productService;
            this.productDetailService = productDetailService;
            this.productSizeService = productSizeService;
            this.productColorService = productColorService;
            this.categoryService = categoryService;
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

        [HttpGet()]
        [Route("api/product/size/list")]
        public async Task<IActionResult> GetAllProductSize()
        {
            var result = new ServiceResponseModel<List<ProductSizeDTO>>();

            try
            {
                result = productSizeService.GetAllProductSize();
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }

        [HttpGet()]
        [Route("api/product/color/list")]
        public async Task<IActionResult> GetAllProductColor()
        {
            var result = new ServiceResponseModel<List<ProductColorDTO>>();

            try
            {
                result = productColorService.GetAllProductColor();
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }
        
        [HttpGet()]
        [Route("api/product/category/list")]
        public async Task<IActionResult> GetAllProductCategory()
        {
            var result = new ServiceResponseModel<List<ProductCategoryDTO>>();

            try
            {
                result = categoryService.GetAllProductCategories();
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }


    }
}
