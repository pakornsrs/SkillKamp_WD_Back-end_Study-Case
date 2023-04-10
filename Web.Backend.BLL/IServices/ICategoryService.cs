using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.DTO.Category;
using Web.Backend.DTO;

namespace Web.Backend.BLL.IServices
{
    public interface ICategoryService
    {
        public ServiceResponseModel<List<ProductCategoryDTO>> GetAllProductCategories();
    }
}
