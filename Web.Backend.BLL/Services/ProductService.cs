using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
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

        private IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile<ModelMapper>()).CreateMapper();

        public ProductService(SkillkampWdStudyCaseDbContext dbContext,
                              IInventoryService inventoryService,
                              IProductDetailService productDetailService,
                              IProductSizeService productSizeService,
                              IProductColorService productColorService)
        {
            this.dbContext = dbContext;
            this.inventoryService = inventoryService;
            this.productDetailService = productDetailService;
            this.productSizeService = productSizeService;
            this.productColorService = productColorService;
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
        public ServiceResponseModel<List<ProductDTO>> SerchProductByKeyword(string Keyword)
        {
            var response = new ServiceResponseModel<List<ProductDTO>>();

            try
            {
                var query = (from q in this.dbContext.Products
                             where q.ProductNameTh.Contains(Keyword) || q.ProductNameEn.Contains(Keyword)
                             join p in this.dbContext.ProductDetails on q.Id equals p.ProductId
                             join i in this.dbContext.ProductInventories on p.InventoryId equals i.Id
                             join d in this.dbContext.DiscountCampeigns on q.DiscountId equals d.Id
                             join s in this.dbContext.ProductSizes on p.SizeId equals s.Id
                             join c in this.dbContext.ProductColors on p.ColorId equals c.Id
                             select new ProductDTO
                             {
                                 Id = q.Id,
                                 CategoryId = q.CategoryId,
                                 ProductDetailId = q.ProductDefaultDetailId,
                                 ProductNameTh = q.ProductNameTh,
                                 ProductNameEn = q.ProductNameEn,
                                 DescTh = q.DescTh,
                                 DescEn = q.DescEn,
                                 Price = p.Price,
                                 CanUseDiscountCode = q.CanUseDiscountCode,
                                 IsDiscount = q.IsDiscount,
                                 DiscountId = q.DiscountId,
                                 DiscountDesc = d.DescTh,
                                 DiscountPercent = d.DisconutPercent,
                                 ColorId = c.Id,
                                 ColorDescTh = c.ColorNameTh,
                                 ColorDescEn = c.ColorNameEn,
                                 SizeId = s.Id,
                                 SizeDescTh = s.SizeDescTh,
                                 SizeDescEn = s.SizeDescEn,
                                 InvertoryId = i.Id,
                                 Quantity = i.Quantity

                             }).ToList();
                
                response.Item = query;

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
    }
}
