using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.DTO;

namespace Web.Backend.BLL.IServices
{
    public interface IProductSizeService
    {
        public ServiceResponseModel<List<ProductSizeDTO>> GetAllProductSize();
        public ServiceResponseModel<ProductSizeDTO> GetProductSizeById(int sizeId);
    }
}
