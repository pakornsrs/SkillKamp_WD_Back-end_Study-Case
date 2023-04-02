using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Backend.DTO.Users
{
    public class UserDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstNameTh { get; set; }

        [Required]
        public string LastNameTh { get; set; }

        [Required]
        public string FirstNameEn { get; set; }

        [Required]
        public string LastNameEn { get; set; }
        public long BirthDate { get; set; }
        public int Gender { get; set; }

        [Required]
        public string TelNo { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
