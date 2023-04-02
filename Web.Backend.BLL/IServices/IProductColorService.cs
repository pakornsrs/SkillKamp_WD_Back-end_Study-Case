using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.DTO.ProductColors;

namespace Web.Backend.BLL.IServices
{
    public interface IProductColorService
    {
        public List<ProductColorDTO> GetAllProductColor();
        public ProductColorDTO GetProductSizeById(int colorId);
    }
}
