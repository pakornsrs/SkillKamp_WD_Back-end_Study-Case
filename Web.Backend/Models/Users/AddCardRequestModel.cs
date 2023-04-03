using Web.Backend.DTO.Cards;

namespace Web.Backend.Models.Users
{
    public class AddCardRequestModel
    {
        public int UserId{get;set;}
        public List<CardRequestDTO> CardDetail {get;set;}
    }
}
