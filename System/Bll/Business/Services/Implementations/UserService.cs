using Business.Mappers;
using Business.Models.Authentication.Dto;
using Business.Models.Authentication.Input;
using Business.Models.User.Dto;
using Business.Services.Interfaces;
using EntityGateWay.Repository.Interfaces;
using SMTP.Models;
using SMTP.Services.Interfaces;
using Utility.Security;

namespace Business.Services.Implementations
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly ISMTPService _sMTPService;

        public UserService(IUserRepository repository, ISMTPService sMTPService)
        {
            _repository = repository;
            _sMTPService = sMTPService;
        }

        public async Task<List<UserDto>?> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            return UserMapper.ToDtoList(users);
        }

        public async Task<UserDto?> GetByIdAsync(Guid id)
        {
            var user = await _repository.GetByIdAsync(id);

            return UserMapper.ToDto(user);
        }

        public async Task<RegistrationDto?> RegistrationUserAsync(UserInput input)
        {
            if (string.IsNullOrWhiteSpace(input.Email) || string.IsNullOrWhiteSpace(input.Name)
                    || string.IsNullOrWhiteSpace(input.Password))
                throw new Exception("Empty data!");
            if (await _repository.IsExistsUserAsync(input.Email))
                return null;

            var hash = HashUtility.HashPassword(input.Password);

            var newUser = UserMapper.ToDBEntity(input, hash.Hash, hash.Salt);
            if (newUser is null)
                throw new Exception("Internal error!");

            var userId = await _repository.RegistrationAsync(newUser);

            return new RegistrationDto
            {
                UserId = userId,
            };
        }

        public async Task<TokenDto?> AuthorizationUserAsync(AuthorizationInput input, string jwtKey, string issuer)
        {
            if (string.IsNullOrWhiteSpace(input.Email) || string.IsNullOrWhiteSpace(input.Password))
                throw new Exception("Empty data!");

            var user = await _repository.GetByEmailAsync(input.Email);
            if (user is null)
                return null;

            var isPasswordValid = HashUtility.VerifyPassword(input.Password, user.PasswordHash, user.Salt);
            if (!isPasswordValid)
                return null;

            var token = JwtUtility.GenerateToken(user.Id.ToString(), user.Email, jwtKey, issuer);

            return new TokenDto
            {
                Token = token,
                UserId = user.Id
            };
        }

        public async Task<bool> UpdateUserAsync(UpdateUserInput input, Guid id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user is null)
                throw new Exception("No user entity.");
            if (user.Email != input.Email && await _repository.IsExistsUserAsync(input.Email))
                return false;

            var newUser = user! with
            {
                Name = input.Name,
                Email = input.Email,
                NumberPhone = input.NumberPhone,
                Type = input.Type,
            };

            await _repository.UpdateAsync(newUser);

            return true;
        }

        public async Task<bool> IsExistsUserAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("Empty data!");

            return await _repository.IsExistsUserAsync(email);
        }

        public async Task<bool> IsExistsUserAsync(Guid id)
        {
            return await _repository.IsExistsUserAsync(id);
        }

        public async Task<bool> SendPasswordResetTokenAsync(HostModel host, string email, string jwtKey, string issuer)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(host.Host)
                    || string.IsNullOrWhiteSpace(host.Username) || string.IsNullOrWhiteSpace(host.Password))
                throw new Exception("Empty data!");

            var user = await _repository.GetByEmailAsync(email);
            if (user is null)
                return false;

            var token = JwtUtility.GenerateToken(user.Id.ToString(), email, jwtKey, issuer);
            _sMTPService.SendPasswordReset(host, email, "https://bonchi", token);

            return true;
        }

        public async Task ResetPasswordAsync(Guid id, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
                throw new Exception("Empty data!");
            var user = await _repository.GetByIdAsync(id);
            if (user is null)
                throw new Exception("No user entity!");

            var hash = HashUtility.HashPassword(newPassword);
            var newUser = user! with
            {
                PasswordHash = hash.Hash,
                Salt = hash.Salt,
            };

            await _repository.UpdateAsync(newUser);
        }

        public async Task DeleteUser(Guid id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user is null)
                throw new Exception("No user entity!");

            var newUser = user! with
            {
                DeleteTime = DateTime.UtcNow,
            };

            await _repository.UpdateAsync(newUser);
        }
    }
}