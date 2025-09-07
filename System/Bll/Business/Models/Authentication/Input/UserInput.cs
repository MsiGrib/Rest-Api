namespace Business.Models.Authentication.Input
{
    public class UserInput
    {
        public required string Name { get; init; }
        public required string Email { get; init; }
        public required string Password { get; init; }
        public string? NumberPhone { get; init; }
    }
}