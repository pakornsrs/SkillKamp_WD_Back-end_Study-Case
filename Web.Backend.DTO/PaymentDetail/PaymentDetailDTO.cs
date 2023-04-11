using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Backend.DTO.PaymentDetail
{
    public class PaymentDetailDTO
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? PaymentMethod { get; set; }
        public int? CardId { get; set; }
        public int? Status { get; set; }
    }
}
