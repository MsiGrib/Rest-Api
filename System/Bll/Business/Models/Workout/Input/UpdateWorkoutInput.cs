namespace Business.Models.Workout.Input
{
    public class UpdateWorkoutInput
    {
        public Guid Id { get; init; }
        public required DateTime StartDate { get; init; }
        public DateTime? EndDate { get; init; }
        public string? Notes { get; init; }
        public List<Guid>? ExerciseIds { get; init; }
    }
}