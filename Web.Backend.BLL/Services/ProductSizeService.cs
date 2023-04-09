using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.BLL.IServices;
using Web.Backend.BLL.ModelMappers;
using Web.Backend.DAL;
using Web.Backend.DTO;
using Web.Backend.DTO.ProductSizes;
using Web.Backend.DTO.Roles;

namespace Web.Backend.BLL.Services
{
    public class ProductSizeService : IProductSizeService
    {
        private readonly SkillkampWdStudyCaseDbContext dbContext;

        private IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile<ModelMapper>()).CreateMapper();

        public ProductSizeService(SkillkampWdStudyCaseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ServiceResponseModel<List<ProductSizeDTO>> GetAllProductSize()
        {
            var response = new ServiceResponseModel<List<ProductSizeDTO>>();

            try
            {
                var query = (from q in this.dbContext.ProductSizes
                             select q).ToList();

                var sizes = new List<ProductSizeDTO>();

                foreach (var item in query)
                {
                    var size = mapper.Map<ProductSizeDTO>(item);
                    sizes.Add(size);
                }

                response.Item = sizes;

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

        public ServiceResponseModel<List<ProductSizeDTO>> GetProductSizeByIds(List<int> sizeIds)
        {
            var response = new ServiceResponseModel<List<ProductSizeDTO>>();

            try
            {
                var query = (from q in this.dbContext.ProductSizes
                             where sizeIds.Contains(q.Id)
                             select q).ToList();

                var sizes = new List<ProductSizeDTO>();

                foreach(var item in query)
                {
                    var size = mapper.Map<ProductSizeDTO>(item);
                    sizes.Add(size);
                }

                response.Item = sizes;

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
    }
}
