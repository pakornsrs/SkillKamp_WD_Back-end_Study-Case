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
using Web.Backend.DTO.ProductRating;
using Web.Backend.DTO.ProductReview;

namespace Web.Backend.BLL.Services
{
    public class ProductReviewService : IProductReviewService
    {
        private readonly SkillkampWdStudyCaseDbContext dbContext;

        private IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile<ModelMapper>()).CreateMapper();

        public ProductReviewService(SkillkampWdStudyCaseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ServiceResponseModel<List<ProductReviewCountDTO>> GetReviewCount(List<int> productIds)
        {
            var response = new ServiceResponseModel<List<ProductReviewCountDTO>>();

            try
            {
                var query = (from q in this.dbContext.ProductReviews
                             where q.ProductId != null && productIds.Contains(q.ProductId.Value)
                             select q).ToList();

                var reviews = new List<ProductReviewCountDTO>();

                for(int i = 0; i < productIds.Count; i++)
                {
                    var review = new ProductReviewCountDTO
                    {
                        ProductId = productIds[i],
                        ReviewCount = query.Where(q => q.ProductId == productIds[i]).Count()
                    };

                    reviews.Add(review);
                }

                response.Item = reviews;

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
