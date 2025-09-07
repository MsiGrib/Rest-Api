using Business.Models.Authentication.Dto;
using Business.Models.Authentication.Input;
using Business.Services.Interfaces;
using DataModel.Enums;
using DataModel.Models.DataBase;
using EntityGateWay.Repository.Interfaces;
using Utility.Security;

namespace Business.Services.Implementations
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<RegistrationDto?> RegistrationUserAsync(UserInput input)
        {
            if (string.IsNullOrWhiteSpace(input.Email) || string.IsNullOrWhiteSpace(input.Name)
                    || string.IsNullOrWhiteSpace(input.Password))
                throw new Exception("Empty data!");
            if (await _repository.IsExistsUserAsync(input.Email))
                return null;

            var hash = HashUtility.HashPassword(input.Password);

            var newUser = new User
            {
                Name = input.Name,
                Email = input.Email,
                PasswordHash = hash.Hash,
                Salt = hash.Salt,
                NumberPhone = input.NumberPhone,
                Type = UserType.Active,
            };

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
    }
}