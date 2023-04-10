using Microsoft.AspNetCore.Mvc;
using Web.Backend.BLL.Services;
using Web.Backend.DTO.Users;
using Web.Backend.DTO;
using Web.Backend.Models.Users;
using Web.Backend.BLL.IServices;
using Web.Backend.DTO.Enums;
using ActionResult = Web.Backend.DTO.Enums.ActionResult;
using Web.Backend.DTO.Cards;
using Microsoft.AspNetCore.Authorization;
using Web.Backend.Models;

namespace Web.Backend.Controllers
{
    public class CardController : Controller
    {
        private readonly IUserCardService userCardService;
        public CardController(IUserCardService userCardService)
        {
            this.userCardService = userCardService;
        }

        [HttpPost()]
        [Route("api/card/add")]
        public async Task<IActionResult> AddUserCard([FromBody] AddCardRequestModel req)
        {
            var result = new ServiceResponseModel<ActionResult>();

            try
            {
                result = userCardService.AddUserCards(req.UserId, req.CardDetail);
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }

        [HttpPost()]
        [Route("api/card/get")]
        public async Task<IActionResult> GetCardByUSerId([FromBody] UserIdRequestModel req)
        {
            var result = new ServiceResponseModel<List<CardResponseDTO>>();

            try
            {
                result = userCardService.GetCardByUserId(req.userId);
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }

        
    }
}
