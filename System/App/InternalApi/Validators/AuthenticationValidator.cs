using Business.Models.Authentication.Input;
using System.Text.RegularExpressions;

namespace InternalApi.Validators
{
    public class AuthenticationValidator : BaseValidator
    {
        public static (bool IsValid, List<string> Errors) Registration(UserInput input)
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

            if (string.IsNullOrWhiteSpace(input.Password))
                errors.Add("Password is required.");

            return (!errors.Any(), errors);
        }

        public static (bool IsValid, List<string> Errors) Authorization(AuthorizationInput input)
        {
            var errors = BaseValidate(input) ?? new List<string>();
            if (!errors.Any())
                return (false, errors);

            if (string.IsNullOrWhiteSpace(input.Email))
                errors.Add("Email is required.");
            else if (!Regex.IsMatch(input.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                errors.Add("Email format is invalid.");

            if (string.IsNullOrWhiteSpace(input.Password))
                errors.Add("Password is required.");

            return (!errors.Any(), errors);
        }

        public static (bool IsValid, List<string> Errors) ForgotPassword(ForgotPasswordInput input)
        {
            var errors = BaseValidate(input) ?? new List<string>();
            if (!errors.Any())
                return (false, errors);

            if (string.IsNullOrWhiteSpace(input.Email))
                errors.Add("Email is required.");
            else if (!Regex.IsMatch(input.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                errors.Add("Email format is invalid.");

            return (!errors.Any(), errors);
        }

        public static (bool IsValid, List<string> Errors) ResetPassword(ResetPasswordInput input)
        {
            var errors = BaseValidate(input) ?? new List<string>();
            if (!errors.Any())
                return (false, errors);

            if (string.IsNullOrWhiteSpace(input.NewPassword))
                errors.Add("NewPassword is required.");

            return (!errors.Any(), errors);
        }
    }
}