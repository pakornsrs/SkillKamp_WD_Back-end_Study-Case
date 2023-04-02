using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Backend.DTO.Users
{
    public class RegistrationDTO
    {
        public int Id { get; set; }
        public string FirstNameTh { get; set; }
        public string LastNameTh { get; set; }
        public string FirstNameEn { get; set; }
        public string LastNameEn { get; set; }
        public DateTime BirthDate { get; set; }
        public int Gender { get; set; }
        public string TelNo { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
        public string UserToken { get; set; }
    }
}
