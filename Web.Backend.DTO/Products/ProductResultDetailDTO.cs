using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Backend.DTO.Products
{
    public class ProductResultDetailDTO
    {
        public int? ProductId { get; set; }
        public int ProductDetailId { get; set; }
        public decimal? Price { get; set; }
        public int? SizeId { get; set; }
        public string SizeDescTh { get; set; }
        public string SizeDescEn { get; set; }
        public int? ColorId { get; set; }
        public string ColorDescTh { get; set; }
        public string ColorDescEn { get; set; }
        public int? InvertoryId { get; set; }
        public int? Quantity { get; set; }
        public string ColorCode { get; set; }
    }
}
