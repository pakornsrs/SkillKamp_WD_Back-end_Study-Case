using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Backend.DTO.PurchastSession
{
    public class PurchaseSessionDTO
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? Expire { get; set; }
        //public decimal? TotalPrice { get; set; }
    }
}
