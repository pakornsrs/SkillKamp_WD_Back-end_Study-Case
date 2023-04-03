using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.DTO;
using Web.Backend.DTO.ProductColors;

namespace Web.Backend.BLL.IServices
{
    public interface IProductColorService
    {
        public ServiceResponseModel<List<ProductColorDTO>> GetAllProductColor();
        public ServiceResponseModel<ProductColorDTO> GetProductSizeById(int colorId);
    }
}
