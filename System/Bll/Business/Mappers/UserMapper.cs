using Business.Models.Authentication.Input;
using Business.Models.User.Dto;
using DataModel.Enums;
using DataModel.Models.DataBase;

namespace Business.Mappers
{
    public static class UserMapper
    {
        public static UserDto? ToDto(User? user)
        {
            if (user is null)
                return null;

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name ?? string.Empty,
                Email = user.Email ?? string.Empty,
                NumberPhone = user.NumberPhone,
                Type = user.Type,
            };
        }

        public static User? ToDBEntity(UserInput? input, string hash, string salt)
        {
            if (input is null || string.IsNullOrWhiteSpace(hash)
                    || string.IsNullOrWhiteSpace(salt))
                return null;

            return new User
            {
                Name = input.Name,
                Email = input.Email,
                PasswordHash = hash,
                Salt = salt,
                NumberPhone = input.NumberPhone,
                Type = UserType.Active,
                DeleteTime = null,
            };
        }

        public static List<UserDto>? ToDtoList(IEnumerable<User>? users)
        {
            if (users is null)
                return null;

            return users
                .Where(u => u != null)
                .Select(ToDto)!
                .Where(dto => dto != null)
                .ToList()!;
        }
    }
}