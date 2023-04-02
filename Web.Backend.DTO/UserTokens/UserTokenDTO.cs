using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Backend.DTO.UserTokens
{
    public class UserTokenDTO
    {
        public int Id { get; set; }
        public string? Token { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? Expire { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
