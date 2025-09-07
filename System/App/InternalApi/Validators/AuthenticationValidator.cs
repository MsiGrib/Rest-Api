using Business.Models.Authentication.Input;
using System.Text.RegularExpressions;

namespace InternalApi.Validators
{
    public static class AuthenticationValidator
    {
        public static (bool IsValid, List<string> Errors) Registration(UserInput input)
        {
            var errors = new List<string>();

            if (input is null)
            {
                errors.Add("Input model is null.");
                return (false, errors);
            }

            if (string.IsNullOrWhiteSpace(input.Name))
                errors.Add("Name is required.");

            if (string.IsNullOrWhiteSpace(input.Email))
                errors.Add("Email is required.");
            else if (!Regex.IsMatch(input.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                errors.Add("Email format is invalid.");

            if (string.IsNullOrWhiteSpace(input.Password))
                errors.Add("Password is required.");

            return (!errors.Any(), errors);
        }

        public static (bool IsValid, List<string> Errors) Authorization(AuthorizationInput input)
        {
            var errors = new List<string>();

            if (input is null)
            {
                errors.Add("Input model is null.");
                return (false, errors);
            }

            if (string.IsNullOrWhiteSpace(input.Email))
                errors.Add("Email is required.");

            if (string.IsNullOrWhiteSpace(input.Password))
                errors.Add("Password is required.");

            return (!errors.Any(), errors);
        }
    }
}