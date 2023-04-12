using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.BLL.IServices;
using Web.Backend.BLL.Services;
using Web.Backend.Controllers;
using Web.Backend.Models;
using Web.Backend.Models.Products;

namespace Web.Backend.Test.Controllers
{
    public class ProductControllerTest
    {
        private readonly IProductService productService;
        private readonly IProductDetailService productDetailService;
        private readonly IProductSizeService productSizeService;
        private readonly IProductColorService productColorService;
        private readonly ICategoryService categoryService;

        public ProductControllerTest()
        {
            productService = A.Fake<IProductService>();
            productDetailService = A.Fake<IProductDetailService> ();
            productSizeService = A.Fake<IProductSizeService> ();
            productColorService = A.Fake<IProductColorService> ();
            categoryService = A.Fake<ICategoryService>();
        }

        [Fact]
        public void AddNewProduct_Response_Is_NotNull()
        {
            var Request = A.Fake<AddProductRequestModel>();
            var controller = new ProductController(productService, productDetailService, productSizeService, productColorService, categoryService);

            var result = controller.AddNewProduct(Request);

            Assert.NotNull(result);
        }

        [Fact]
        public void AddAditionalDetail_Response_Is_NotNull()
        {
            var Request = A.Fake<AddNewProductDetailRequestModel>();
            var controller = new ProductController(productService, productDetailService, productSizeService, productColorService, categoryService);

            var result = controller.AddAditionalDetail(Request);

            Assert.NotNull(result);
        }

        [Fact]
        public void SerchProductByKeyword_Response_Is_NotNull()
        {
            var Request = A.Fake<SearchProductRequestMode>();
            var controller = new ProductController(productService, productDetailService, productSizeService, productColorService, categoryService);

            var result = controller.SerchProductByKeyword(Request);

            Assert.NotNull(result);
        }

        [Fact]
        public void GetNewArrival_Response_Is_NotNull()
        {
            var controller = new ProductController(productService, productDetailService, productSizeService, productColorService, categoryService);

            var result = controller.GetNewArrival();

            Assert.NotNull(result);
        }

        [Fact]
        public void GetProductFullDetail_Response_Is_NotNull()
        {
            var Request = A.Fake<ProductIdRequestModel>();
            var controller = new ProductController(productService, productDetailService, productSizeService, productColorService, categoryService);

            var result = controller.GetProductFullDetail(Request);

            Assert.NotNull(result);
        }

        [Fact]
        public void GetAllProductSize_Response_Is_NotNull()
        {
            var controller = new ProductController(productService, productDetailService, productSizeService, productColorService, categoryService);

            var result = controller.GetAllProductSize();

            Assert.NotNull(result);
        }

        [Fact]
        public void GetAllProductColor_Response_Is_NotNull()
        {
            var controller = new ProductController(productService, productDetailService, productSizeService, productColorService, categoryService);

            var result = controller.GetAllProductColor();

            Assert.NotNull(result);
        }

        [Fact]
        public void GetAllProductCategories_Response_Is_NotNull()
        {
            var controller = new ProductController(productService, productDetailService, productSizeService, productColorService, categoryService);

            var result = controller.GetAllProductCategories();

            Assert.NotNull(result);
        }

    }
}
