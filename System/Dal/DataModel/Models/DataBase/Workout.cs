namespace DataModel.Models.DataBase
{
    /// <summary>
    /// Сессия тренеровки пользователя
    /// </summary>
    public class Workout : IEntity<Guid>
    {
        public Guid Id { get; init; }
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        public string? Notes { get; init; }

        public required Guid UserId { get; init; }
        public virtual User? User { get; init; }
    }
}