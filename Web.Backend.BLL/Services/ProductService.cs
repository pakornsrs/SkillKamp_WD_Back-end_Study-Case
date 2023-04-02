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

        public async Task<ServiceResponseModel<ProductDTO>> AddNewProduct(AddProductDTO productReq,ProductDetailDTO productDetailReq,InventoryDTO inventoryReq)
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

                    var inventory = inventoryService.CreateInventory(inventoryReq);

                    // Inser data to tabe [Product_Detail]

                    productDetailReq.ProductId = product.Id;
                    productDetailReq.InventoryId = inventory.Id;
                    var productDetail = productDetailService.CreateProductDetail(productDetailReq);

                    // Update table [Product]

                    product.ProductDetailId = productDetail.Id;

                    dbContext.Set<Product>().Update(product);
                    dbContext.SaveChanges();

                    // Create response item

                    var responseItem = mapper.Map<ProductDTO>(product);
                    var productSize = productSizeService.GetProductSizeById(productDetail.SizeId.Value);
                    var productColor = productColorService.GetProductSizeById(productDetail.ColorId.Value);

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
                    throw;
                }
            }

            return response;
        }
    }
}
