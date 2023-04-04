using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Web.Backend.DTO.ProductDetails
{
    public class ProductDetailResponseDTO
    {
        public int? Id { get; set; } = null;
        public decimal? Price { get; set; }
        public bool? IsActive { get; set; }
        public int? SizeId { get; set; }
        public int? ColorId { get; set; }
        public string? ImagePath { get; set; }
    }
}
