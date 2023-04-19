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
    [Authorize]
    public class CardController : Controller
    {
        private readonly IUserCardService userCardService;
        public CardController(IUserCardService userCardService)
        {
            this.userCardService = userCardService;
        }

        /// <summary>
        /// (To add credit/debit card on registration)
        /// </summary>
        /// <remarks>
        /// Detail
        /// 
        ///     User can add credit/debit card using this path.
        ///     
        /// </remarks>
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

        /// <summary>
        /// (To get user registed credit/debit card)
        /// </summary>
        /// <remarks>
        /// Detail
        /// 
        ///     To get the credit/debit card that user gives on the registration process. 
        ///     The cards will be displayed on product summary page then user can select them for payment.
        ///     
        /// </remarks>
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
