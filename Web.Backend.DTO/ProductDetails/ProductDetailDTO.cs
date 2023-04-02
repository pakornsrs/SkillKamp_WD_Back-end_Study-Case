using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Web.Backend.DTO.ProductDetails
{
    public class ProductDetailDTO
    {
        [JsonIgnore]
        public int? Id { get; set; } = null;

        [JsonIgnore]
        public int? InventoryId { get; set; } = null;

        [JsonIgnore]
        public int? ProductId { get; set; } = null;
        public decimal? Price { get; set; }
        public bool? IsActive { get; set; }
        public int? SizeId { get; set; } = null;
        public int? ColorId { get; set; } = null;
        public string? ImagePath { get; set; }

    }
}
