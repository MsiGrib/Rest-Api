using Business.Models.Authentication.Input;
using System.Text.RegularExpressions;

namespace InternalApi.Validators
{
    public class UserValidator : BaseValidator
    {
        public static (bool IsValid, List<string> Errors) UpdateUser(UpdateUserInput input)
        {
            var errors = BaseValidate(input) ?? new List<string>();
            if (!errors.Any())
                return (false, errors);

            if (string.IsNullOrWhiteSpace(input.Name))
                errors.Add("Name is required.");

            if (string.IsNullOrWhiteSpace(input.Email))
                errors.Add("Email is required.");
            else if (!Regex.IsMatch(input.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                errors.Add("Email format is invalid.");

            return (!errors.Any(), errors);
        }
    }
}