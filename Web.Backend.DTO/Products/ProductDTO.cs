using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Backend.DTO.Products
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public int? ProductDetailId { get; set; }
        public string? ProductNameTh { get; set; }
        public string? ProductNameEn { get; set; }
        public string? DescTh { get; set; }
        public string? DescEn { get; set; }
        public decimal? Price { get; set; }
        public bool? CanUseDiscountCode { get; set; } = true;
        public bool? IsDiscount { get; set; } = false;
        public int? DiscountId { get; set; }
        public string DiscountDesc { get; set; }
        public decimal? DiscountPercent { get; set; }
        public  int SizeId { get; set; }
        public string SizeDescTh { get; set; }
        public string SizeDescEn { get; set; }
        public int ColorId { get; set; }
        public string ColorDescTh { get; set; }
        public string ColorDescEn { get; set; }
        public int InvertoryId { get; set; }
        public int? Quantity { get; set; }

    }
}
