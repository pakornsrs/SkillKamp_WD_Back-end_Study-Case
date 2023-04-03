using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.DTO;
using Web.Backend.DTO.Cards;
using Web.Backend.DTO.Enums;

namespace Web.Backend.BLL.IServices
{
    public interface IUserCardService
    {
        public ServiceResponseModel<List<CardResponseDTO>> GetCardByUserId(int userId);
        public ServiceResponseModel<ActionResult> AddUserCards(int userId, List<CardRequestDTO> cardList);
    }
}
