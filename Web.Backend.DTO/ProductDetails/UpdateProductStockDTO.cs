using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Web.Backend.DTO.ProductDetails
{
    public class UpdateProductStockDTO
    {
        public int? Id { get; set; } = null;
        public int? InventoryId { get; set; } = null;
        public int? ProductId { get; set; } = null;
        public decimal? Price { get; set; }
        public int Quantity { get; set; }
        public bool? IsActive { get; set; }
    }
}
