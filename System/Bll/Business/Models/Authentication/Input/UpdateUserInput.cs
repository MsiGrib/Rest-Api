using DataModel.Enums;

namespace Business.Models.Authentication.Input
{
    public class UpdateUserInput
    {
        public required string Name { get; init; }
        public required string Email { get; init; }
        public string? NumberPhone { get; init; }
        public required UserType Type { get; init; }
    }
}