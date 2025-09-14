namespace Business.Models.Workout.Input
{
    public class WorkoutInput
    {
        public required DateTime StartDate { get; init; }
        public string? Notes { get; init; }
        public List<Guid>? ExerciseIds { get; init; }
    }
}