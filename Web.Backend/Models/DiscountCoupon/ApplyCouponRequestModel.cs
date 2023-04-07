namespace Web.Backend.Models.DiscountCoupon
{
    public class ApplyCouponRequestModel
    {
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public string CouponCode { get; set; }
    }
}
