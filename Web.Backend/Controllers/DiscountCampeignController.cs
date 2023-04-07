using Microsoft.AspNetCore.Mvc;
using Web.Backend.BLL.IServices;
using Web.Backend.BLL.Services;
using Web.Backend.DTO.Cards;
using Web.Backend.DTO;
using Web.Backend.Models.Campeigns;
using Web.Backend.DTO.Campeigns;

namespace Web.Backend.Controllers
{
    public class DiscountCampeignController : Controller
    {
        private readonly IDiscountCampeignService discountCampeignService;
        public DiscountCampeignController(IDiscountCampeignService discountCampeignService)
        {
            this.discountCampeignService = discountCampeignService;
        }

        [HttpPost()]
        [Route("api/campeign/add")]
        public async Task<IActionResult> AddDiscountCampeign([FromBody] AddOrUpdateCampeignRequestModel req)
        {
            var result = new ServiceResponseModel<CampeignsDTO>();

            try
            {
                result = discountCampeignService.AddDiscountCampeign(req.CampeignDetail);
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }

        [HttpPost()]
        [Route("api/campeign/update")]
        public async Task<IActionResult> UpdateDiscountCampeign([FromBody] AddOrUpdateCampeignRequestModel req)
        {
            var result = new ServiceResponseModel<CampeignsDTO>();

            try
            {
                result = discountCampeignService.UpdateDiscountCampeign(req.CampeignDetail);
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }

        [HttpPost()]
        [Route("api/campeign/terminate")]
        public async Task<IActionResult> TerminateDiscountCampeign([FromBody] TerminateCampeignRequestModel req)
        {
            var result = new ServiceResponseModel<string>();

            try
            {
                result = discountCampeignService.TerminateDiscountCampeign(req.CampeignIds);
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }
    }
}
