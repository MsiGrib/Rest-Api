using Business.Models.Exercise.Dto;
using Business.Models.Exercise.Input;
using Business.Services.Interfaces;
using InternalApi.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseService _exerciseService;

        public ExerciseController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ExerciseDto>?>> GetAllExercise([FromQuery] ExerciseFilterInput? input = null)
        {
            var exercises = await _exerciseService.GetAllAsync(input);

            return Ok(exercises);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExerciseDto?>> GetExercise([FromRoute] Guid id)
        {
            var exercise = await _exerciseService.GetByIdAsync(id);
            if (exercise is null)
                return NotFound(new { Errors = "This exercise is not fount." });

            return Ok(exercise);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddNewExercise([FromBody] ExerciseInput input)
        {
            var validResult = ExerciseValidator.AddNewExercise(input);
            if (!validResult.IsValid)
                return BadRequest(new { Errors = validResult.Errors! });

            var success = await _exerciseService.AddAsync(input);
            if (!success)
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Errors = "Failed create Exercise."
                });

            return Ok(new { Info = "Success create." });
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> UpdateExercise(UpdateExerciseInput input)
        {
            var validResult = ExerciseValidator.UpdateExercise(input);
            if (!validResult.IsValid)
                return BadRequest(new { Errors = validResult.Errors! });

            var success = await _exerciseService.UpdateExerciseAsync(input);
            if (!success)
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Errors = "Failed create Exercise."
                });

            return Ok(new { Info = "Success update." });
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteExercise([FromRoute] Guid id)
        {
            bool success = await _exerciseService.DeleteExerciseAsync(id);
            if (!success)
                return NotFound(new { Errors = "Exercise not found." });

            return Ok(new { Info = "Exercise is soft deleted." });
        }
    }
}