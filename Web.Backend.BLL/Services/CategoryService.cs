using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.BLL.IServices;
using Web.Backend.DAL;
using Web.Backend.DTO.ProductColors;
using Web.Backend.DTO;
using Web.Backend.DTO.Category;
using Web.Backend.BLL.ModelMappers;

namespace Web.Backend.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly SkillkampWdStudyCaseDbContext dbContext;

        private IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile<ModelMapper>()).CreateMapper();
        public CategoryService(SkillkampWdStudyCaseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ServiceResponseModel<List<ProductCategoryDTO>> GetAllProductCategories()
        {
            var response = new ServiceResponseModel<List<ProductCategoryDTO>>();

            try
            {
                var query = (from q in this.dbContext.ProductCategories
                             select q).ToList();

                var categories = new List<ProductCategoryDTO>();

                foreach (var item in query)
                {
                    var category = mapper.Map<ProductCategoryDTO>(item);
                    categories.Add(category);
                }

                response.Item = categories;

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
