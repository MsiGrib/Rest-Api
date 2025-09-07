namespace Business.Models.Authentication.Dto
{
    public class TokenDto
    {
        public string Token { get; set; } = string.Empty;
        public Guid UserId { get; set; }
    }
}