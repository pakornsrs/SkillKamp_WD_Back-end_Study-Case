using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.BLL.IServices;
using Web.Backend.BLL.ModelMappers;
using Web.Backend.BLL.UtilityMethods;
using Web.Backend.DAL;
using Web.Backend.DAL.Entities;
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

        public ServiceResponseModel<DefaultResponseModel> CreatrProductReview(int userId, int prodId, decimal rating, string reviewerName, string text, bool isRecommend )
        {
            var response = new ServiceResponseModel<DefaultResponseModel>();
            var defaultMode = new DefaultResponseModel();
            var tranDateTime = DateTimeUtility.GetDateTimeThai();

            using (IDbContextTransaction transaction = this.dbContext.Database.BeginTransaction())
            {
                try
                {
                    // Check review statuc

                    var queryReview = (from q in dbContext.ProductReviews
                                       where q.UserId == userId && q.ProductId == prodId
                                       select q ).FirstOrDefault();

                    if(queryReview != null)
                    {
                        response.ErrorCode = "CR0001";
                        response.ErrorMessage = "You already given a review for this product.";

                        return response;
                    }

                    // Create review

                    var review = new ProductReview();

                    review.UserId = userId;
                    review.ProductId = prodId;
                    review.Rating = rating;
                    review.ReviewerName = reviewerName;
                    review.ReviewerText = text;
                    review.IsRecomment = isRecommend;
                    review.CreateDate = tranDateTime;
                    review.UpdateDate = tranDateTime;
                    review.CreateBy = reviewerName;
                    review.UpdateBy = reviewerName;

                    dbContext.Set<ProductReview>().Add(review);
                    dbContext.SaveChanges();

                    // Update rating

                    var productId = new List<int> { prodId };
                    var reviewCount = GetReviewCount(productId);

                    if (reviewCount.IsError)
                    {
                        response.ErrorCode = "CR0002";
                        response.ErrorMessage = "Cannot get review count.";

                        return response;
                    }

                    var count = reviewCount.Item[0].ReviewCount;

                    var queryRating = (from q in dbContext.ProductRatings
                                       where q.ProductId == prodId
                                       select q).FirstOrDefault();

                    if (queryRating == null)
                    {
                        // Create new one
                        queryRating = new ProductRating();

                        queryRating.ProductId = prodId;
                        queryRating.Rating = review.Rating;
                        queryRating.CreateDate = tranDateTime;
                        queryRating.CreateBy = "system";
                        queryRating.UpdateDate = tranDateTime;
                        queryRating.UpdateBy = "system";

                        dbContext.Set<ProductRating>().Add(queryRating);
                        dbContext.SaveChanges();
                    }
                    else
                    {
                        // Update 

                        queryRating.Rating = (queryRating.Rating + review.Rating) / count;
                        queryRating.UpdateDate = tranDateTime;
                        queryRating.UpdateBy = "system";

                        dbContext.Set<ProductRating>().Update(queryRating);
                        dbContext.SaveChanges();
                    }

                    defaultMode.message = "Success";
                    response.Item = defaultMode;

                    response.ErrorCode = "0000";
                    response.ErrorMessage = "Success";

                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    response.ErrorCode = "BE9999";
                    response.ErrorMessage = "Interal server error.";
                }
            }

            return response;
        }
    }
}
