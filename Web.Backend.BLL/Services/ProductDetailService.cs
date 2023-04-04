using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
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

namespace Web.Backend.BLL.Services
{
    public class ProductDetailService : IProductDetailService
    {
        private readonly SkillkampWdStudyCaseDbContext dbContext;
        private readonly IInventoryService inventoryService;

        private IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile<ModelMapper>()).CreateMapper();

        public ProductDetailService(SkillkampWdStudyCaseDbContext dbContext, IInventoryService inventoryService)
        {
            this.dbContext = dbContext;
            this.inventoryService = inventoryService;
        }

        public ServiceResponseModel<List<ProductDetailDTO>> CreateProductDetail(List<ProductDetailDTO> req)
        {
            var response = new ServiceResponseModel<List<ProductDetailDTO>>();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            try
            {
                var productDetails = new List<ProductDetail>();

                foreach(var item in req)
                {
                    var productDetail = mapper.Map<ProductDetail>(item);

                    productDetail.CreateDate = tranDateTime;
                    productDetail.UpdateDate = tranDateTime;
                    productDetail.CreateBy = "system";
                    productDetail.UpdateBy = "system";

                    productDetails.Add(productDetail);
                }

                dbContext.Set<ProductDetail>().AddRange(productDetails);
                dbContext.SaveChanges();

                var result = new List<ProductDetailDTO>();

                foreach(var item in productDetails)
                {
                    var _result = mapper.Map<ProductDetailDTO>(item);
                    result.Add(_result);
                }
                response.Item = result;

                response.ErrorCode = "0000";
                response.ErrorMessage = "Success";
            }
            catch (Exception ex)
            {
                response.ErrorCode = "BE9999";
                response.ErrorMessage = "Interal server error.";
            }

            return response;
        }

        public ServiceResponseModel<DefaultResponseModel> AddAditionalDetail(List<ProductDetailDTO> req)
        {
            var response = new ServiceResponseModel<DefaultResponseModel>();
            var defaultModel = new DefaultResponseModel();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            using (IDbContextTransaction transaction = this.dbContext.Database.BeginTransaction())
            {
                try
                {
                    var invertories = new List<InventoryDTO>();

                    foreach (var item in req)
                    {
                        var invertory = new InventoryDTO();
                        invertory.Quantity = item.Quantity;

                        invertories.Add(invertory);
                    }

                    var inventoryServiceResponse = inventoryService.CreateInventory(invertories);

                    if (inventoryServiceResponse.IsError)
                    {
                        transaction.Rollback();

                        response.ErrorCode = "AD0001";
                        response.ErrorMessage = "Cannot create inventory";

                        return response;
                    }

                    invertories = inventoryServiceResponse.Item;

                    for(int i =0; i < invertories.Count; i++)
                    {
                        req[i].InventoryId = invertories[i].Id;
                    }

                    var createDetailResult = CreateProductDetail(req);

                    if (createDetailResult.IsError)
                    {
                        transaction.Rollback();

                        response.ErrorCode = "AD0002";
                        response.ErrorMessage = "Add new product detail error";

                        return response;
                    }

                    defaultModel.message = "Add new product detail success.";
                    response.Item = defaultModel;

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
    }
}
