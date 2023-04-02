using System.ComponentModel.DataAnnotations;
using Web.Backend.DAL.Entities;
using Web.Backend.DTO.Addresses;
using Web.Backend.DTO.Cards;
using Web.Backend.DTO.Users;

namespace Web.Backend.Models.Users
{
    public class RegistrationRequestModel
    {
        [Required]
        public UserDTO User { get; set; }

        public List<AddressDTO> UserAddress { get; set; }
        public List<CardDTO> UserCard { get; set; }
    }
}
