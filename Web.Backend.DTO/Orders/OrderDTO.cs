using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Backend.DTO.Orders
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int? SessionId { get; set; }
        public string? CartItem { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? CouponId { get; set; }
        public decimal percentDiscount { get; set; } = 0;
        public DateTime? CreateDate { get; set; }

    }
}
