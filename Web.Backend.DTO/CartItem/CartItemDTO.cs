using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Backend.DTO.CartItem
{
    public class CartItemDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int? SessionId { get; set; }
        public int? ProductId { get; set; }
        public int? productDetailId { get; set; }
        public int? Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
