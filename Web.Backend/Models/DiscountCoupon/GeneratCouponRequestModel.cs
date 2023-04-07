namespace Web.Backend.Models.DiscountCoupon
{
    public class GeneratCouponRequestModel
    {
        public int UserId { get; set; }
        public int Type { get; set; }
        public decimal PercenDiscount { get; set; }
        public int Limitation { get; set; }
    }
}
