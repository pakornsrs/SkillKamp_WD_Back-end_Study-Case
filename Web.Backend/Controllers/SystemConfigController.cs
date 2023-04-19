using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Web.Backend.BLL.IServices;
using Web.Backend.BLL.Services;
using Web.Backend.BLL.UtilityMethods;
using Web.Backend.DTO;
using Web.Backend.DTO.Config;
using Web.Backend.Filters;
using Web.Backend.Jwt;

namespace Web.Backend.Controllers
{
    public class SystemConfigController : Controller
    {
        private readonly ISystemConfigService systemConfigService;
        private readonly JwtConfig jwtConfig;
        public SystemConfigController(ISystemConfigService systemConfigService, IOptions<JwtConfig> jwtConfig)
        {
            this.systemConfigService = systemConfigService;
            this.jwtConfig = jwtConfig.Value;
        }

        /// <summary>
        /// (To create jet token)
        /// </summary>
        /// <remarks>
        /// Detail
        /// 
        ///     Use to create token for setvice testing.
        ///     
        /// </remarks>
        [HttpGet()]
        //[TypeFilter(typeof(TokenLifetimeFilter))]
        [Route("api/config/token")]
        public async Task<IActionResult> CreateToken()
        {
            var result = new ServiceResponseModel<string>();

            try
            {
                var jwt = new JwtTokenGenerator();
                var expireDate = DateTimeUtility.GetDateTimeThai().AddHours(12);

                var token = jwt.GenerateToken(999, "test-token", "test-token", expireDate, jwtConfig);

                result.Item = token;
                result.ErrorCode = "0000";
                result.ErrorMessage = "success";

            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }

        [HttpGet()]
        [Route("api/config/api-version")]
        public async Task<IActionResult> GetServiceVersion()
        {
            var result = new ServiceResponseModel<GetConfigDTO<string>>();

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

        /// <summary>
        /// (To create review)
        /// </summary>
        /// <remarks>
        /// Detail
        /// 
        ///     This api use to get img path that show in slidshow.
        ///     
        /// </remarks>
        [HttpGet()]
        [Route("api/config/slide/path")]
        public async Task<IActionResult> GetSlide()
        {
            var result = new ServiceResponseModel<GetConfigDTO<List<string>>>();

            try
            {
                result = systemConfigService.GetSlideImgPath();
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }

        /// <summary>
        /// (To check Jwt)
        /// </summary>
        /// <remarks>
        /// Detail
        /// 
        ///     This api use to check token. if token was expire, force user to sign off from system.
        ///     
        /// </remarks>
        [HttpGet()]
        [Authorize]
        //[TypeFilter(typeof(TokenLifetimeFilter))]
        [Route("api/config/token/check")]
        public async Task<IActionResult> GetTokenValidCheck()
        {
            var result = new ServiceResponseModel<bool>();

            try
            {
                result.ErrorCode = "0000";
                result.ErrorMessage = "Token is valid";
                result.Item = true;
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }
    }
}
