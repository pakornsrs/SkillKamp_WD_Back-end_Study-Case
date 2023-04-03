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

        public ServiceResponseModel<List<ProductColorDTO>> GetAllProductColor()
        {
            var response = new ServiceResponseModel<List<ProductColorDTO>>();

            try
            {
                var query = (from q in this.dbContext.ProductColors
                             select q).ToList();

                var colors = new List<ProductColorDTO>();

                foreach (var item in query)
                {
                    var color = mapper.Map<ProductColorDTO>(item);
                    colors.Add(color);
                }

                response.Item = colors;

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

        public ServiceResponseModel<ProductColorDTO> GetProductSizeById(int colorId)
        {
            var response = new ServiceResponseModel<ProductColorDTO>();

            try
            {
                var query = (from q in this.dbContext.ProductColors
                             where q.Id == colorId
                             select q).FirstOrDefault();

                response.Item = mapper.Map<ProductColorDTO>(query);

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
