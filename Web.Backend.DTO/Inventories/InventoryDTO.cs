using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Web.Backend.DTO.Inventories
{
    public class InventoryDTO
    {
        [JsonIgnore]
        public int? Id { get; set; } = null;
        public int Quantity { get; set; }
    }
}
