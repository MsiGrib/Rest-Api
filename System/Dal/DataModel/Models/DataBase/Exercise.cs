namespace DataModel.Models.DataBase
{
    public record class Exercise : IEntity<Guid>
    {
        public Guid Id { get; init; }
        public required string Name { get; init; }
        public required int Sets { get; init; }
        public required int Reps { get; init; }
        public required double Weight { get; init; }
    }
}