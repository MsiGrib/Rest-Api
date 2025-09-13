using DataModel.Enums;

namespace Business.Models.User.Dto
{
    public record class UserDto
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
        public required string Email { get; init; }
        public string? NumberPhone { get; init; }
        public required UserType Type { get; init; }
    }
}