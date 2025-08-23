using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Services.Interfaces;
using APIAUTH.Domain.Entities;
using APIAUTH.Infrastructure.Services;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Messaging;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace APIAUTH.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly NotificationServiceInfra _notificationService;
        private readonly IAccountService _userService;

        public AccountController(IAccountService userService, NotificationServiceInfra notificationService)
        {
            _userService = userService;
            _notificationService = notificationService;
        }

        [HttpPost("recoverPassword")]
        public async Task<IActionResult> RecoverPassword(string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _userService.RecoverPassword(email);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("changePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(UserPasswordDto userPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;

                userPasswordDto.Email = email;

                return Ok(await _userService.ChangePassword(userPasswordDto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
