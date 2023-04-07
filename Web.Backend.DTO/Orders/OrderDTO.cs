using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Backend.DTO.Orders
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int? SessionId { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime? CreateDate { get; set; }

    }
}
