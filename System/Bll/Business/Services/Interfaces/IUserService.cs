using Business.Models.Authentication.Dto;
using Business.Models.Authentication.Input;

namespace Business.Services.Interfaces
{
    public interface IUserService
    {
        public Task<RegistrationDto?> RegistrationUserAsync(UserInput input);
        public Task<TokenDto?> AuthorizationUserAsync(AuthorizationInput input, string jwtKey, string issuer);
    }
}