namespace Business.Models.Authentication.Dto
{
    public record class TokenDto
    {
        public string Token { get; set; } = string.Empty;
        public Guid UserId { get; set; }
    }
}