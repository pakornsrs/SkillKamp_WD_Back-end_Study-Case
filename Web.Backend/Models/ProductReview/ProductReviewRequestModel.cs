namespace Web.Backend.Models.ProductReview
{
    public class ProductReviewRequestModel
    {
        public int UserId { get; set; }
        public int ProdId { get; set; }
        public decimal Rating { get; set; }
        public string ReviewerName { get; set; }
        public string Text { get; set; }
        public bool IsRecommend { get; set; }
    }
}
