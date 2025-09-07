using Business.Models.Authentication.Dto;
using Business.Models.Authentication.Input;
using Business.Models.User.Dto;
using Business.Models.User.Input;
using SMTP.Models;

namespace Business.Services.Interfaces
{
    public interface IUserService
    {
        public Task<List<UserDto>?> GetAllAsync(UserFilterInput? input = null);
        public Task<UserDto?> GetByIdAsync(Guid id);
        public Task<RegistrationDto?> RegistrationUserAsync(UserInput input);
        public Task<TokenDto?> AuthorizationUserAsync(AuthorizationInput input, string jwtKey, string issuer);
        public Task<bool> UpdateUserAsync(UpdateUserInput input, Guid id);
        public Task<bool> IsExistsUserAsync(string email);
        public Task<bool> IsExistsUserAsync(Guid id);
        public Task<bool> SendPasswordResetTokenAsync(HostModel host, string email, string jwtKey, string issuer);
        public Task ResetPasswordAsync(Guid id, string newPassword);
        public Task DeleteUser(Guid id);
    }
}