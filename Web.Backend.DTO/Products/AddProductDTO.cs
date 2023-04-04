using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Backend.DTO.Products
{
    public class AddProductDTO
    {
        public bool? IsMultiDetail { get; set; }
        public int? CategoryId { get; set; } = null;
        public string? ProductNameTh { get; set; }
        public string? ProductNameEn { get; set; }
        public string? DescTh { get; set; }
        public string? DescEn { get; set; }
        public bool? CanUseDiscountCode { get; set; } = true;
        public bool? IsDiscount { get; set; } = false;
        public int? DiscountId { get; set; } = null;
    }
}
