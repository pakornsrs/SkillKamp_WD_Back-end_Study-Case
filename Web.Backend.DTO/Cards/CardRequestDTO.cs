using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Backend.DTO.Cards
{
    public class CardRequestDTO
    {
        public string? CardNo { get; set; }
        public string? NameOnCard { get; set; }
        public int? Provider { get; set; }
        public DateTime? CardExpireDate { get; set; }
    }
}
