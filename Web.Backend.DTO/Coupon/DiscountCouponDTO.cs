using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Backend.DTO.Coupon
{
    public class DiscountCouponDTO
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string? CouponCode { get; set; }
        public int? Type { get; set; }
        public decimal? PercentDiscount { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string? ExpireDateText => ExpireDate.Value.ToString("dd/MMM/yyyy");
        public int? UseCount { get; set; }
        public int? Limitation { get; set; }
    }
}
