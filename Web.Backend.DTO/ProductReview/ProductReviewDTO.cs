using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Backend.DTO.ProductReview
{
    public class ProductReviewDTO
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? ProductId { get; set; }
        public int? Rating { get; set; }
        public string? ReviewerName { get; set; }
        public string? ReviewerText { get; set; }
        public bool? IsRecomment { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
