using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Backend.DTO.Addresses
{
    public class AddressDTO
    {
        public int Id { get; set; }
        [Required]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        [Required]
        public string Province { get; set; }

        [Required]
        public string District { get; set; }

        [Required]
        public string Subdistrict { get; set; }

        [Required]
        public string ZipCode { get; set; }
    }
}
