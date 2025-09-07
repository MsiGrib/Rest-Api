namespace DataModel.Models.DataBase
{
    public record class WorkoutExercise : IEntity<long>
    {
        public long Id { get; init; }

        public required Guid WorkoutId { get; init; }
        public virtual Workout? Workout { get; init; }

        public required Guid ExerciseId { get; init; }
        public virtual Exercise? Exercise { get; init; }
    }
}