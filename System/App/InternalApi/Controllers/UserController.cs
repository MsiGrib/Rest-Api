using Business.Models.Authentication.Input;
using Business.Models.User.Dto;
using Business.Services.Interfaces;
using InternalApi.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace InternalApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>?>> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();

            return Ok(users);
        }

        [HttpGet("current")]
        public async Task<ActionResult<UserDto?>> GetUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst(JwtRegisteredClaimNames.Sub);
            if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
                return Unauthorized(new { Errors = "Invalid token." });

            var user = await _userService.GetByIdAsync(userId);

            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult> FullUpdateUser([FromBody] UpdateUserInput input)
        {
            var validResult = UserValidator.UpdateUser(input);
            if (!validResult.IsValid)
                return BadRequest(new { Errors = validResult.Errors! });
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst(JwtRegisteredClaimNames.Sub);
            if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
                return Unauthorized(new { Errors = "Invalid token." });
            if (!await _userService.IsExistsUserAsync(userId))
                return NotFound(new { Errors = "User not found." });

            bool success = await _userService.UpdateUserAsync(input, userId);
            if (!success)
                return BadRequest(new { Errors = "This email is exist, it wont be possible to update." });

            return Ok(new { Info = "User is updeting." });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser([FromRoute] Guid id)
        {
            if (!await _userService.IsExistsUserAsync(id))
                return NotFound(new { Errors = "User not found." });

            await _userService.DeleteUser(id);

            return Ok(new { Info = "User is soft deleted." });
        }
    }
}