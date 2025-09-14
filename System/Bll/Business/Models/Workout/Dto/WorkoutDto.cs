using Business.Models.User.Dto;

namespace Business.Models.Workout.Dto
{
    public record class WorkoutDto
    {
        public Guid Id { get; init; }
        public required DateTime StartDate { get; init; }
        public DateTime? EndDate { get; init; }
        public string? Notes { get; init; }

        public required Guid UserId { get; init; }
        public virtual UserDto? User { get; init; }
    }
}