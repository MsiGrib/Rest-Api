using Business.Models.User.Dto;
using Business.Models.User.Input;
using Business.Services.Interfaces;
using InternalApi.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace InternalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>?>> GetAllUsers([FromQuery] UserFilterInput? input = null)
        {
            var users = await _userService.GetAllAsync(input);

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto?>> GetUser([FromRoute] Guid id)
        {
            if (!await _userService.IsExistsUserAsync(id))
                return NotFound(new { Errors = "This user is not fount." });

            var user = await _userService.GetByIdAsync(id);

            return Ok(user);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUserInput input)
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

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> DeleteUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst(JwtRegisteredClaimNames.Sub);
            if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
                return Unauthorized(new { Errors = "Invalid token." });
            if (!await _userService.IsExistsUserAsync(userId))
                return NotFound(new { Errors = "User not found." });

            await _userService.DeleteUser(userId);

            return Ok(new { Info = "User is soft deleted." });
        }
    }
}