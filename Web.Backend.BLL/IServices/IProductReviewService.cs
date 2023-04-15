using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.DTO.ProductReview;
using Web.Backend.DTO;

namespace Web.Backend.BLL.IServices
{
    public interface IProductReviewService
    {
        public ServiceResponseModel<List<ProductReviewCountDTO>> GetReviewCount(List<int> productIds);
        public ServiceResponseModel<DefaultResponseModel> CreatrProductReview(int userId, int prodId, decimal rating, string reviewerName, string text, bool isRecommend);
    }
}
