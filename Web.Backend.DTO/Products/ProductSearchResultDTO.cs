using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Backend.DTO.Products
{
    public class ProductSearchResultDTO
    {
        public int ProductId { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryDescTh { get; set; }
        public string? CategoryDescEn { get; set; }
        public int? ProductDefaultDetailId { get; set; }
        public string? ProductNameTh { get; set; }
        public string? ProductNameEn { get; set; }
        public string? ProductDescTh { get; set; }
        public string? ProductDescEn { get; set; }
        public int Rating { get; set; }
        public int ReviewCount { get; set; }
        public bool? CanUseDiscountCode { get; set; } = true;
        public bool? IsDiscount { get; set; } = false;
        public int? DiscountId { get; set; }
        public string DiscountDescTh { get; set; }
        public string DiscountDescEn { get; set; }
        public decimal? DiscountPercent { get; set; }
        public bool? IsMultiDetail { get; set; }
        public int ProductTypeCount => IsMultiDetail == false ? 1 : SepcificDetail.Count();

        public List<ProductResultDetailDTO> SepcificDetail { get; set; } = new List<ProductResultDetailDTO>();

    }
}
