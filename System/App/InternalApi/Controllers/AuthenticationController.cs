using Business.Models.Authentication.Dto;
using Business.Models.Authentication.Input;
using Business.Services.Interfaces;
using InternalApi.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMTP.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace InternalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly BasicConfiguration _basicConfiguration;

        public AuthenticationController(IUserService userService, BasicConfiguration basicConfiguration)
        {
            _userService = userService;
            _basicConfiguration = basicConfiguration;
        }

        [HttpPost("registration")]
        public async Task<ActionResult<RegistrationDto>> Registration([FromBody] UserInput input)
        {
            var validResult = AuthenticationValidator.Registration(input);
            if (!validResult.IsValid)
                return BadRequest(new { Errors = validResult.Errors! });
            var registration = await _userService.RegistrationUserAsync(input);
            if (registration is null)
                return Conflict(new { Errors = "User with this email already exists." });

            return Ok(registration);
        }

        [HttpPost("authorization")]
        public async Task<ActionResult<TokenDto>> Authorization([FromBody] AuthorizationInput input)
        {
            var validResult = AuthenticationValidator.Authorization(input);
            if (!validResult.IsValid)
                return BadRequest(new { Errors = validResult.Errors! });
            var authorization = await _userService.AuthorizationUserAsync(input, _basicConfiguration.Key, _basicConfiguration.Issuer);
            if (authorization is null)
                return Unauthorized(new { Errors = "Invalid Email or password." });

            return Ok(authorization);
        }

        [HttpPost("forgot-password")]
        public async Task<ActionResult> ForgotPassword([FromBody] ForgotPasswordInput input)
        {
            var validResult = AuthenticationValidator.ForgotPassword(input);
            if (!validResult.IsValid)
                return BadRequest(new { Errors = validResult.Errors! });

            var host = new HostModel
            {
                Host = _basicConfiguration.SMTPHost,
                Port = _basicConfiguration.SMTPPort,
                UseSsl = _basicConfiguration.UseSMTPSsl,
                Username = _basicConfiguration.SMTPUsername,
                Password = _basicConfiguration.SMTPPassword,
            };

            bool success = await _userService.SendPasswordResetTokenAsync(host, input.Email, _basicConfiguration.Key, _basicConfiguration.Issuer);
            if (!success)
                return NotFound(new { Errors = "This email is not exist." });

            return Ok(new { Info = "If the email exists, a password reset link has been sent." });
        }

        [HttpPost("reset-password")]
        [Authorize]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordInput input)
        {
            var validResult = AuthenticationValidator.ResetPassword(input);
            if (!validResult.IsValid)
                return BadRequest(new { Errors = validResult.Errors! });
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst(JwtRegisteredClaimNames.Sub);
            if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
                return Unauthorized(new { Errors = "Invalid token." });

            await _userService.ResetPasswordAsync(userId, input.NewPassword);

            return Ok("Password has been reset successfully");
        }
    }
}