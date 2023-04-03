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

        public ServiceResponseModel<ProductDTO> AddNewProduct(AddProductDTO productReq,ProductDetailDTO productDetailReq,InventoryDTO inventoryReq)
        {
            var response = new ServiceResponseModel<ProductDTO>();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            using (IDbContextTransaction transaction = this.dbContext.Database.BeginTransaction())
            {
                try
                {
                    // Insert data to table [Product]

                    var product = mapper.Map<Product>(productReq);

                    product.CreateDate = tranDateTime;
                    product.UpdateDate = tranDateTime;
                    product.CreateBy = "system";
                    product.UpdateBy = "system";

                    dbContext.Set<Product>().Add(product);
                    dbContext.SaveChanges();  

                    // Insert data to table [Product]

                    var inventoryServiceResponse = inventoryService.CreateInventory(inventoryReq);

                    if (inventoryServiceResponse.IsError)
                    {
                        response.ErrorCode = "AP0001";
                        response.ErrorMessage = "Cannot add product inventory.";
                    }

                    var inventory = inventoryServiceResponse.Item;

                    // Inser data to tabe [Product_Detail]

                    productDetailReq.ProductId = product.Id;
                    productDetailReq.InventoryId = inventory.Id;
                    var productDetailServiceResponse = productDetailService.CreateProductDetail(productDetailReq);

                    if (productDetailServiceResponse.IsError)
                    {
                        response.ErrorCode = "AP0002";
                        response.ErrorMessage = "Cannot add product detail.";
                    }

                    var productDetail = productDetailServiceResponse.Item;

                    // Update table [Product]

                    product.ProductDetailId = productDetail.Id;

                    dbContext.Set<Product>().Update(product);
                    dbContext.SaveChanges();

                    // Create response item

                    var responseItem = mapper.Map<ProductDTO>(product);

                    var productSizeServiceResponse = productSizeService.GetProductSizeById(productDetail.SizeId.Value);

                    if (productSizeServiceResponse.IsError)
                    {
                        response.ErrorCode = "AP0002";
                        response.ErrorMessage = "Cannot add product detail.";
                    }

                    var productSize = productSizeServiceResponse.Item;

                    var productColorServiceResponse = productColorService.GetProductSizeById(productDetail.ColorId.Value);

                    if (productColorServiceResponse.IsError)
                    {
                        response.ErrorCode = "AP0002";
                        response.ErrorMessage = "Cannot add product detail.";
                    }

                    var productColor = productColorServiceResponse.Item;

                    responseItem.SizeId = productDetail.SizeId.Value;
                    responseItem.ColorId = productDetail.ColorId.Value;
                    responseItem.InvertoryId = productDetail.InventoryId.Value;
                    responseItem.Price = productDetail.Price.Value;
                    responseItem.Quantity = inventory.Quantity;
                    responseItem.ColorDescTh = productColor.ColorNameTh;
                    responseItem.ColorDescEn = productColor.ColorNameEn;
                    responseItem.SizeDescTh = productSize.SizeDescTh;
                    responseItem.SizeDescEn = productSize.SizeDescEn;

                    response.Item = responseItem;

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
                                 ProductDetailId = q.ProductDetailId,
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
