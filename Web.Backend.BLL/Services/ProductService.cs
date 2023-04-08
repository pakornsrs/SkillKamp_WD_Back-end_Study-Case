using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.BLL.IServices;
using Web.Backend.BLL.ModelMappers;
using Web.Backend.BLL.UtilityMethods;
using Web.Backend.DAL;
using Web.Backend.DAL.Entities;
using Web.Backend.DTO;
using Web.Backend.DTO.Inventories;
using Web.Backend.DTO.ProductDetails;
using Web.Backend.DTO.ProductRating;
using Web.Backend.DTO.ProductReview;
using Web.Backend.DTO.Products;
using Web.Backend.DTO.Users;

namespace Web.Backend.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly SkillkampWdStudyCaseDbContext dbContext;
        private readonly IInventoryService inventoryService;
        private readonly IProductDetailService productDetailService;
        private readonly IProductSizeService productSizeService;
        private readonly IProductColorService productColorService;
        private readonly IProductReviewService productReviewService;
        private readonly IProductRatingService productRatingService;

        private IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile<ModelMapper>()).CreateMapper();

        public ProductService(SkillkampWdStudyCaseDbContext dbContext,
                              IInventoryService inventoryService,
                              IProductDetailService productDetailService,
                              IProductSizeService productSizeService,
                              IProductColorService productColorService,
                              IProductReviewService productReviewService,
                              IProductRatingService productRatingService)
        {
            this.dbContext = dbContext;
            this.inventoryService = inventoryService;
            this.productDetailService = productDetailService;
            this.productSizeService = productSizeService;
            this.productColorService = productColorService;
            this.productReviewService = productReviewService;
            this.productRatingService = productRatingService;
        }

        public ServiceResponseModel<DefaultResponseModel> AddNewProduct(AddProductDTO productReq,List<ProductDetailDTO> productDetailReq,List<InventoryDTO> inventoryReq)
        {
            var response = new ServiceResponseModel<DefaultResponseModel>();
            var defaultMode = new DefaultResponseModel();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            using (IDbContextTransaction transaction = this.dbContext.Database.BeginTransaction())
            {
                try
                {
                    // Check data
                    if(productDetailReq.Count != inventoryReq.Count)
                    {
                        response.ErrorCode = "AP0001";
                        response.ErrorMessage = "In correct request data.";

                        return response;
                    }

                    // Insert data to table [Product]

                    var product = mapper.Map<Product>(productReq);

                    product.DiscountId = 0;
                    product.CreateDate = tranDateTime;
                    product.UpdateDate = tranDateTime;
                    product.CreateBy = "system";
                    product.UpdateBy = "system";

                    dbContext.Set<Product>().Add(product);
                    dbContext.SaveChanges();  

                    // Insert data to table [Inventory]

                    var inventoryServiceResponse = inventoryService.CreateInventory(inventoryReq);

                    if (inventoryServiceResponse.IsError)
                    {
                        response.ErrorCode = "AP0002";
                        response.ErrorMessage = "Cannot add product inventory.";

                        return response;
                    }

                    var inventories = inventoryServiceResponse.Item;

                    // Inser data to tabe [Product_Detail]

                    for (int i = 0; i < inventories.Count; i++)
                    {
                        productDetailReq[i].ProductId = product.Id;
                        productDetailReq[i].InventoryId = inventories[i].Id;
                    }

                    var productDetailServiceResponse = productDetailService.CreateProductDetail(productDetailReq);

                    if (productDetailServiceResponse.IsError)
                    {
                        response.ErrorCode = "AP0003";
                        response.ErrorMessage = "Cannot add product detail.";

                        return response;
                    }

                    var productDetails = productDetailServiceResponse.Item;

                    // Update table [Product]

                    if (product.IsMultiDetail.Value)
                    {
                        // multi detail
                        foreach(var productDetail in productDetails)
                        {
                            if (product.ProductDefaultDetailId == null) product.ProductDefaultDetailId = productDetail.Id;
                        }
                    }
                    else
                    {
                        foreach (var productDetail in productDetails)
                        {
                            product.ProductDefaultDetailId = productDetail.Id;
                        }
                    }

                    dbContext.Set<Product>().Update(product);
                    dbContext.SaveChanges();

                    defaultMode.message = "Add product success";
                    response.Item = defaultMode;

                    response.ErrorCode = "0000";
                    response.ErrorMessage = "Success";

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    response.ErrorCode = "BE9999";
                    response.ErrorMessage = "Internal server error.";
                }
            }

            return response;
        }
        public ServiceResponseModel<DefaultResponseModel> UpdateStockProduct(List<UpdateProductStockDTO> productDetailUpdateReq)
        {
            var response = new ServiceResponseModel<DefaultResponseModel>();
            var defaultMode = new DefaultResponseModel();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            using (IDbContextTransaction transaction = this.dbContext.Database.BeginTransaction())
            {
                try
                {

                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    response.ErrorCode = "BE9999";
                    response.ErrorMessage = "Internal server error.";
                }
            }

            return response;
        }
        public ServiceResponseModel<List<ProductSearchResultDTO>> SerchProductByKeyword(string Keyword)
        {
            var response = new ServiceResponseModel<List<ProductSearchResultDTO>>();

            try
            {
                // Query product group

                var productQuery = (from product in this.dbContext.Products
                                    where product.ProductNameTh.Contains(Keyword) || product.ProductNameEn.Contains(Keyword) || product.DescTh.Contains(Keyword) || product.DescEn.Contains(Keyword)
                                    join discount in this.dbContext.DiscountCampeigns on product.DiscountId equals discount.Id
                                    join category in this.dbContext.ProductCategories on product.CategoryId equals category.Id
                                    select new ProductSearchResultDTO
                                    {
                                        ProductId = product.Id,
                                        CategoryId = product.CategoryId,
                                        CategoryDescTh = category.DescTh,
                                        CategoryDescEn = category.DescEn,
                                        ProductDefaultDetailId = product.ProductDefaultDetailId,
                                        ProductNameTh = product.ProductNameTh,
                                        ProductNameEn = product.ProductNameEn,
                                        ProductDescTh = product.DescTh,
                                        ProductDescEn = product.DescEn,
                                        CanUseDiscountCode = product.CanUseDiscountCode,
                                        IsDiscount = product.IsDiscount ,
                                        DiscountId = discount.Id,
                                        DiscountDescTh = discount.DescTh,
                                        DiscountDescEn = discount.DescEn,
                                        DiscountPercent = discount.DisconutPercent,
                                        IsMultiDetail = product.IsMultiDetail,
                                    })
                                    .GroupBy(obj => obj.ProductId)
                                    .Select(obj => obj.First())
                                    .ToList();

                var multiDetailProducIds = new List<int>();
                var productIds = new List<int>();

                foreach (var item in productQuery)
                {
                    productIds.Add(item.ProductId);

                    if (!item.IsMultiDetail.Value) continue;

                    multiDetailProducIds.Add(item.ProductId);
                }

                // Query rating
                var ratings = GetRating(productIds);

                // Query review count
                var reviews = GetReviewCount(productIds);

                // Query product detail
                var productDetail = GetProductDetails(multiDetailProducIds);

                // response model merge
                var productSearchResponse = GetResponseSearchResult(productQuery, ratings, reviews, productDetail);

                response.Item = productSearchResponse;

                response.ErrorCode = "0000";
                response.ErrorMessage = "Success";
            }
            catch (Exception ex)
            {
                response.ErrorCode = "BE9999";
                response.ErrorMessage = "Internal server error.";
            }

            return response;
        }
        public ServiceResponseModel<List<ProductSearchResultDTO>> GetNewArrival()
        {
            var response = new ServiceResponseModel<List<ProductSearchResultDTO>>();

            try
            {

                var productQuery = (from product in this.dbContext.Products
                                    join discount in this.dbContext.DiscountCampeigns on product.DiscountId equals discount.Id
                                    join category in this.dbContext.ProductCategories on product.CategoryId equals category.Id
                                    join detail in this.dbContext.ProductDetails on product.ProductDefaultDetailId equals detail.Id
                                    orderby product.CreateDate descending
                                    select new ProductSearchResultDTO
                                    {
                                        ProductId = product.Id,
                                        CategoryId = product.CategoryId,
                                        CategoryDescTh = category.DescTh,
                                        CategoryDescEn = category.DescEn,
                                        ProductDefaultDetailId = product.ProductDefaultDetailId,
                                        ProductNameTh = product.ProductNameTh,
                                        ProductNameEn = product.ProductNameEn,
                                        ProductDescTh = product.DescTh,
                                        ProductDescEn = product.DescEn,
                                        CanUseDiscountCode = product.CanUseDiscountCode,
                                        IsDiscount = product.IsDiscount,
                                        DiscountId = discount.Id,
                                        DiscountDescTh = discount.DescTh,
                                        DiscountDescEn = discount.DescEn,
                                        DiscountPercent = discount.DisconutPercent,
                                        IsMultiDetail = product.IsMultiDetail,
                                        PriceStart = detail.Price.Value,
                                        DefaultImgPaht = detail.ImagePath,
                                        Created = product.CreateDate
                                    })
                                    //.GroupBy(obj => obj.ProductId)
                                    //.Select(obj => obj.First())
                                    .Take(5)
                                    .ToList();

                var multiDetailProducIds = new List<int>();
                var productIds = new List<int>();

                foreach (var item in productQuery)
                {
                    productIds.Add(item.ProductId);

                    if (!item.IsMultiDetail.Value) continue;

                    multiDetailProducIds.Add(item.ProductId);
                }

                // Query rating
                var ratings = GetRating(productIds);

                // Query review count
                var reviews = GetReviewCount(productIds);

                // Query product detail
                var productDetail = GetProductDetails(multiDetailProducIds);

                // response model merge
                var productSearchResponse = GetResponseSearchResult(productQuery, ratings, reviews, productDetail);

                response.Item = productSearchResponse;

                response.ErrorCode = "0000";
                response.ErrorMessage = "Success";
            }
            catch (Exception ex)
            {
                response.ErrorCode = "BE9999";
                response.ErrorMessage = "Internal server error.";
            }

            return response;
        }
        public ServiceResponseModel<int?> GetAllProductCount()
        {
            var response = new ServiceResponseModel<int?>();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            try
            {
                var query = (from q in this.dbContext.Products
                             where q.IsActive == true
                             select q).Count();

                response.Item = query;

                response.ErrorCode = "0000";
                response.ErrorMessage = "Success";
            }
            catch (Exception ex)
            {
                response.Item = null;

                response.ErrorCode = "BE9999";
                response.ErrorMessage = "Internal server error.";
            }

            return response;
        }


        private List<ProductRatingDTO> GetRating (List<int> productIds)
        {
            try
            {
                var ratingResponse = productRatingService.GetProductRating(productIds);
                var ratings = ratingResponse.Item;

                return ratings;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private List<ProductReviewCountDTO> GetReviewCount(List<int> productIds)
        {
            try
            {
                var reviewsResponse = productReviewService.GetReviewCount(productIds);
                var reviews = reviewsResponse.Item;

                return reviews;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private List<ProductResultDetailDTO> GetProductDetails(List<int> multiDetailProducIds)
        {
            try
            {
                var productDetail = new List<ProductResultDetailDTO>();

                if (multiDetailProducIds.Count > 0)
                {
                    var detailQuery = (from detail in this.dbContext.ProductDetails
                                       where multiDetailProducIds.Contains(detail.ProductId.Value)
                                       join color in this.dbContext.ProductColors on detail.ColorId equals color.Id
                                       select new ProductResultDetailDTO
                                       {
                                           ProductId = detail.ProductId,
                                           ProductDetailId = detail.Id,
                                           ColorId = detail.ColorId,
                                           ColorDescTh = color.ColorNameTh,
                                           ColorDescEn = color.ColorNameTh,
                                           ColorCode = color.ColorCode,

                                       })
                                       .GroupBy(obj => obj.ColorId)
                                       .Select(obj => obj.First())
                                       .ToList();

                    productDetail = detailQuery.GroupBy(obj => obj.ProductDetailId).Select(obj => obj.First()).ToList();
                }

                return productDetail;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private List<ProductSearchResultDTO> GetResponseSearchResult(List<ProductSearchResultDTO> productQuery, List<ProductRatingDTO> ratings, List<ProductReviewCountDTO> reviews, List<ProductResultDetailDTO> productDetail)
        {
            try
            {
                foreach (var product in productQuery)
                {
                    var reviewCount = reviews.Where(review => review.ProductId == product.ProductId).FirstOrDefault();
                    product.ReviewCount = reviewCount.ReviewCount;

                    var rating = ratings.Where(rate => rate.ProductId == product.ProductId).FirstOrDefault();
                    product.Rating = rating.Rating.Value;

                    if (!product.IsMultiDetail.Value) continue;

                    var details = productDetail.Where(item => item.ProductId == product.ProductId).ToList();

                    foreach (var detail in details)
                    {
                        product.SepcificDetail.Add(detail);
                    }
                }

                return productQuery;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
