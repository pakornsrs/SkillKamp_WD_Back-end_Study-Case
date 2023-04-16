using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Backend.DTO.ProductReview
{
    public class PurchastedProductDTO
    {
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int CartItemId { get; set; }
        public int? ProductDetailId { get; set; }
        public string? ProductNameTh { get; set; }
        public string? ProductNameEn { get; set; }
        public string? DescTh { get; set; }
        public string? DescEn { get; set; }
        public decimal? Price { get; set; }
        public int SizeId { get; set; }
        public string SizeDescTh { get; set; }
        public string SizeDescEn { get; set; }
        public int ColorId { get; set; }
        public string ColorDescTh { get; set; }
        public string ColorDescEn { get; set; }
        public string? ImagePath { get; set; }
        public string ColorCode { get; set; }
        public int Quantity { get; set; }
        public bool IsReview => ReviewDetail != null;
        public ProductReviewDTO? ReviewDetail { get; set; }
    }
}
