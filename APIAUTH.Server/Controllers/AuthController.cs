﻿using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIAUTH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          
            try
            {
                var tokens = await _authenticationService.AuthenticateUserAsync(request.Email, request.Password);
                return Ok(tokens);
            }
            catch (UnauthorizedAccessException e)
            {
                return Unauthorized("Invalid credentials." + e.Message);
            }
        }

        //TODO: Implementar la autenticacion por SSO 

        [HttpPost("loginSSO")]
        public async Task<IActionResult> LoginSSO(string idTokenGoogle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var tokens = await _authenticationService.AuthenticateWithGoogleAsync(idTokenGoogle);
                return Ok(tokens);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

    }
}