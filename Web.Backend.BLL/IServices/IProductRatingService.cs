using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.DTO.ProductRating;
using Web.Backend.DTO;

namespace Web.Backend.BLL.IServices
{
    public interface IProductRatingService
    {
        public ServiceResponseModel<List<ProductRatingDTO>> GetProductRating(List<int> productIds);
    }
}
