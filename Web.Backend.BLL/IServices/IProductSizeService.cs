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
        public List<ProductSizeDTO> GetAllProductSize();
        public ProductSizeDTO GetProductSizeById(int sizeId);
    }
}
