using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Backend.DTO.Cards
{
    public class CardDTO
    {
        public string? CardNo { get; set; }
        public string? NameOnCard { get; set; }
        public DateTime? CardExpireDate { get; set; }
        public int? CardProvider { get; set; }
    }
}
