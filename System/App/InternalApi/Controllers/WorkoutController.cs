using Business.Models.Workout.Dto;
using Business.Models.Workout.Input;
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
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;

        public WorkoutController(IWorkoutService workoutService)
        {
            _workoutService = workoutService;
        }

        [HttpGet("my")]
        [Authorize]
        public async Task<ActionResult<List<WorkoutDto>?>> GetByUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst(JwtRegisteredClaimNames.Sub);
            if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
                return Unauthorized(new { Errors = "Invalid token." });

            var workouts = await _workoutService.GetAllByUserIdAsync(userId);

            return Ok(workouts);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<WorkoutDto?>> StartWorkout([FromBody] WorkoutInput input)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst(JwtRegisteredClaimNames.Sub);
            if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
                return Unauthorized(new { Errors = "Invalid token." });
            var validResult = WorkoutValidator.StartWorkout(input);
            if (!validResult.IsValid)
                return BadRequest(new { Errors = validResult.Errors! });

            var workout = await _workoutService.StartWorkoutAsync(input, userId);

            return Ok(workout);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<WorkoutDto?>> UpdateWorkout([FromBody] UpdateWorkoutInput input)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst(JwtRegisteredClaimNames.Sub);
            if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
                return Unauthorized(new { Errors = "Invalid token." });
            var validResult = WorkoutValidator.UpdateWorkout(input);
            if (!validResult.IsValid)
                return BadRequest(new { Errors = validResult.Errors! });

            var newWorkout = await _workoutService.UpdateWorkoutAsync(input, userId);

            return Ok(newWorkout);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteWorkout([FromRoute] Guid id)
        {
            bool success = await _workoutService.DeleteWorkoutAsync(id);
            if (!success)
                return NotFound(new { Errors = "This workout is not fount." });

            return Ok(new { Info = "Workout is soft deleted." });
        }
    }
}