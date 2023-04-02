using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.BLL.IServices;
using Web.Backend.BLL.ObjectMappers;
using Web.Backend.DAL;
using Web.Backend.DAL.Entities;
using Web.Backend.DTO;
using Web.Backend.DTO.Roles;

namespace Web.Backend.BLL.Services
{
    public class ProductSizeService : IProductSizeService
    {
        private readonly SkillkampWdStudyCaseDbContext dbContext;

        private IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile<ObjectMapper>()).CreateMapper();

        public ProductSizeService(SkillkampWdStudyCaseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<ProductSizeDTO> GetAllProductSize()
        {
            var response = new List<ProductSizeDTO>();

            try
            {
                var query = (from q in this.dbContext.ProductSizes
                             select q).ToList();

                foreach (var item in query)
                {
                    var size = mapper.Map<ProductSizeDTO>(item);
                    response.Add(size);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return response;
        }

        public ProductSizeDTO GetProductSizeById(int sizeId)
        {
            var response = new ProductSizeDTO();

            try
            {
                var query = (from q in this.dbContext.ProductSizes
                             where q.Id == sizeId
                             select q).FirstOrDefault();

                response = mapper.Map<ProductSizeDTO>(query);

            }
            catch (Exception ex)
            {

                throw;
            }

            return response;
        }
    }
}
