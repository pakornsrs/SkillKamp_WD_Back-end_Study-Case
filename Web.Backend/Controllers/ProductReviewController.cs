using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Backend.BLL.IServices;
using Web.Backend.DTO;
using Web.Backend.Models.ProductReview;
using Web.Backend.Models.Users;

namespace Web.Backend.Controllers
{
    [Authorize]
    public class ProductReviewController : Controller
    {
        private readonly IProductReviewService productReviewService;
        public ProductReviewController(IProductReviewService productReviewService)
        {
            this.productReviewService = productReviewService;
        }

        /// <summary>
        /// (To create review)
        /// </summary>
        /// <remarks>
        /// Detail
        /// 
        ///     This api use when user review product.
        ///     
        /// </remarks>
        [HttpPost()]
        [Route("api/product/review/create")]
        public async Task<IActionResult> CreatrProductReview([FromBody] ProductReviewRequestModel req)
        {
            var result = new ServiceResponseModel<DefaultResponseModel>();

            try
            {
                result = productReviewService.CreatrProductReview(req.UserId, req.ProdId, req.ProdDetailId, req.OrderId, req.Rating,req.ReviewerName, req.Text, req.IsRecommend);
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }
    }
}
