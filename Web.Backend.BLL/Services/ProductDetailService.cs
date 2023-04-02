using AutoMapper;
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
using Web.Backend.DTO.Inventories;
using Web.Backend.DTO.ProductDetails;

namespace Web.Backend.BLL.Services
{
    public class ProductDetailService : IProductDetailService
    {
        private readonly SkillkampWdStudyCaseDbContext dbContext;
        private IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile<ModelMapper>()).CreateMapper();

        public ProductDetailService(SkillkampWdStudyCaseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ProductDetailDTO CreateProductDetail(ProductDetailDTO req)
        {
            var response = new ProductDetailDTO();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            try
            {
                var productDetail = mapper.Map<ProductDetail>(req);

                productDetail.CreateDate = tranDateTime;
                productDetail.UpdateDate = tranDateTime;
                productDetail.CreateBy = "system";
                productDetail.UpdateBy = "system";

                dbContext.Set<ProductDetail>().Add(productDetail);
                dbContext.SaveChanges();

                response = mapper.Map<ProductDetailDTO>(productDetail);
            }
            catch (Exception ex)
            {

                throw;
            }

            return response;
        }
    }
}
