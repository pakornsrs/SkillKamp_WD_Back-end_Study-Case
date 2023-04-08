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
using Web.Backend.DTO.Roles;

namespace Web.Backend.BLL.Services
{
    public class ProductRatingService : IProductRatingService
    {
        private readonly SkillkampWdStudyCaseDbContext dbContext;

        private IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile<ModelMapper>()).CreateMapper();

        public ProductRatingService(SkillkampWdStudyCaseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ServiceResponseModel<List<ProductRatingDTO>> GetProductRating(List<int> productIds)
        {
            var response = new ServiceResponseModel<List<ProductRatingDTO>>();

            try
            {
                var query = (from q in this.dbContext.ProductRatings
                             where q.ProductId!= null && productIds.Contains(q.ProductId.Value)
                             select q).ToList();

                var ratings = new List<ProductRatingDTO>();

                for (int i = 0; i < productIds.Count; i++)
                {
                    var item = query.Where(q => q.ProductId == productIds[i]).FirstOrDefault();
                    var rate = new ProductRatingDTO
                    {
                        ProductId = productIds[i],
                        Rating = item == null ? 0 : item.Rating
                    };

                    ratings.Add(rate);
                }

                response.Item = ratings;

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
