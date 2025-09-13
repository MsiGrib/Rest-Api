using Business.Models.Exercise.Input;

namespace InternalApi.Validators
{
    public class ExerciseValidator : BaseValidator
    {
        public static (bool IsValid, List<string> Errors) AddNewExercise(ExerciseInput input)
        {
            var errors = BaseValidate(input) ?? new List<string>();
            if (!errors.Any())
                return (false, errors);

            if (string.IsNullOrWhiteSpace(input.Name))
                errors.Add("Name is required.");

            return (!errors.Any(), errors);
        }

        public static (bool IsValid, List<string> Errors) UpdateExercise(UpdateExerciseInput input)
        {
            var errors = BaseValidate(input) ?? new List<string>();
            if (!errors.Any())
                return (false, errors);

            if (string.IsNullOrWhiteSpace(input.Name))
                errors.Add("Name is required.");

            return (!errors.Any(), errors);
        }
    }
}