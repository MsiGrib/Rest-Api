using Business.Models.Authentication.Dto;
using Business.Models.Authentication.Input;
using Business.Services.Interfaces;
using InternalApi.Validators;
using Microsoft.AspNetCore.Mvc;

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
                return BadRequest(new { Errors = validResult.Errors });

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
                return BadRequest(new { Errors = validResult.Errors });

            var authorization = await _userService.AuthorizationUserAsync(input, _basicConfiguration.Key, _basicConfiguration.Issuer);
            if (authorization is null)
                return Unauthorized(new { Errors = "Invalid Email or password." });

            return Ok(authorization);
        }
    }
}