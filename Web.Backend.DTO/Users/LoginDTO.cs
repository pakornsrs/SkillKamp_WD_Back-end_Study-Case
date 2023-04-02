using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Backend.DTO.Users
{
    public class LoginDTO
    {
        public UserLoginDTO User { get; set; }
        public string UserToken { get; set; }
    }
}
