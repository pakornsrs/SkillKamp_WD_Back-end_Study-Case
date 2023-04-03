using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.DTO.Addresses;
using Web.Backend.DTO.Cards;

namespace Web.Backend.DTO.Users
{
    public class UserLoginDTO
    {
        public int Id { get; set; }
        public string FirstNameTh { get; set; }
        public string LastNameTh { get; set; }
        public string FirstNameEn { get; set; }
        public string LastNameEn { get; set; }
        public string TelNo { get; set; }
        public string Email { get; set; }
        public List<AddressDTO> AddressList { get; set; }
        public List<CardRequestDTO> CardList { get; set; }
    }
}
