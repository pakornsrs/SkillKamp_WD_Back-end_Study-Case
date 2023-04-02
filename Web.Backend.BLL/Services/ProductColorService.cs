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
using Web.Backend.DTO.ProductColors;

namespace Web.Backend.BLL.Services
{
    public class ProductColorService : IProductColorService
    {
        private readonly SkillkampWdStudyCaseDbContext dbContext;

        private IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile<ModelMapper>()).CreateMapper();

        public ProductColorService(SkillkampWdStudyCaseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<ProductColorDTO> GetAllProductColor()
        {
            var response = new List<ProductColorDTO>();

            try
            {
                var query = (from q in this.dbContext.ProductColors
                             select q).ToList();

                foreach (var item in query)
                {
                    var color = mapper.Map<ProductColorDTO>(item);
                    response.Add(color);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return response;
        }

        public ProductColorDTO GetProductSizeById(int colorId)
        {
            var response = new ProductColorDTO();

            try
            {
                var query = (from q in this.dbContext.ProductColors
                             where q.Id == colorId
                             select q).FirstOrDefault();

                response = mapper.Map<ProductColorDTO>(query);

            }
            catch (Exception ex)
            {

                throw;
            }

            return response;
        }
    }
}
