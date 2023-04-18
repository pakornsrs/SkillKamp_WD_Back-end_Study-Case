﻿using Microsoft.AspNetCore.Mvc;
using Web.Backend.BLL.IServices;
using Web.Backend.BLL.Services;
using Web.Backend.DTO.Config;
using Web.Backend.DTO;
using Web.Backend.Models.Users;
using Web.Backend.DTO.Users;
using Web.Backend.DTO.Enums;
using ActionResult = Web.Backend.DTO.Enums.ActionResult;
using Web.Backend.Jwt;
using Web.Backend.BLL.UtilityMethods;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Web.Backend.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly JwtConfig jwtConfig;
        public UserController(IUserService userService, IOptions<JwtConfig> jwtConfig)
        {
            this.userService = userService;
            this.jwtConfig = jwtConfig.Value;
        }

        [HttpPost()]
        [Route("api/user/registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrationRequestModel req)
        {
            var result = new ServiceResponseModel<RegistrationDTO>();

            try
            {
                result = userService.Registration(req.User, req.UserAddress, req.UserCard);
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }

        [HttpPost()]
        [Route("api/user/login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel req)
        {
            var result = new ServiceResponseModel<LoginDTO>();

            try
            {
                result = userService.Login(req.Username, req.Password);

                if(!result.IsError)
                {
                    var test = jwtConfig;
                    var user = result.Item.User;
                    var jwt = new JwtTokenGenerator(); 
                    var token = jwt.GenerateToken(user.Id, req.Username, "user", DateTimeUtility.GetDateTimeThai().AddMinutes(2),jwtConfig);

                    result.Item.UserToken = token;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }

    }
}
