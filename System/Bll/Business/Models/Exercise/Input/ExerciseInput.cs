namespace Business.Models.Exercise.Input
{
    public class ExerciseInput
    {
        public required string Name { get; init; }
        public required int Sets { get; init; }
        public required int Reps { get; init; }
        public required double Weight { get; init; }
    }
}