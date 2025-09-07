namespace Business.Models.Authentication.Input
{
    public class AuthorizationInput
    {
        public required string Email { get; init; }
        public required string Password { get; init; }
    }
}