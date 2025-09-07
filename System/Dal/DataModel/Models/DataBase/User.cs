using DataModel.Enums;

namespace DataModel.Models.DataBase
{
    public record class User : IEntity<Guid>
    {
        public Guid Id { get; init; }
        public required string Name { get; init; }
        public required string Email { get; init; }
        public required string PasswordHash { get; init; }
        public required string Salt { get; init; }
        public string? NumberPhone { get; init; }
        public required UserType Type { get; init; }
        public DateTime? DeleteTime { get; init; }

        public virtual ICollection<Workout> Workouts { get; init; } = new List<Workout>();
    }
}