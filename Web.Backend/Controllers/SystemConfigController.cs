using Microsoft.AspNetCore.Mvc;
using Web.Backend.BLL.IServices;
using Web.Backend.BLL.Services;
using Web.Backend.DTO;
using Web.Backend.DTO.Config;

namespace Web.Backend.Controllers
{
    public class SystemConfigController : Controller
    {
        private readonly ISystemConfigService systemConfigService;
        public SystemConfigController(ISystemConfigService systemConfigService)
        {
            this.systemConfigService = systemConfigService;
        }

        [HttpGet()]
        [Route("api/config/api-version")]
        public async Task<IActionResult> GetServiceVersion()
        {
            var result = new ServiceResponseModel<GetServiceVersionDTO>();

            try
            {
                result = systemConfigService.GetVersion();
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }
    }
}
