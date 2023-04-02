﻿using Microsoft.AspNetCore.Mvc;
using Web.Backend.BLL.IServices;
using Web.Backend.BLL.Services;
using Web.Backend.DTO.Config;
using Web.Backend.DTO;
using Web.Backend.Models.Users;
using Web.Backend.DTO.Users;

namespace Web.Backend.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost()]
        [Route("api/user/registration")]
        public async Task<IActionResult> GetServiceVersion([FromBody] RegistrationRequestModel req)
        {
            var result = new ServiceResponseModel<RegistrationDTO>();

            try
            {
                result = await userService.Registration(req.User, req.UserAddress, req.UserCard);
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
                result = await userService.Login(req.Username, req.Password);
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(200, result);
        }

    }
}
