using Business.Models.Workout.Input;

namespace InternalApi.Validators
{
    public class WorkoutValidator : BaseValidator
    {
        public static (bool IsValid, List<string> Errors) StartWorkout(WorkoutInput input)
        {
            var errors = BaseValidate(input) ?? new List<string>();
            if (!errors.Any())
                return (false, errors);

            return (!errors.Any(), errors);
        }

        public static (bool IsValid, List<string> Errors) UpdateWorkout(UpdateWorkoutInput input)
        {
            var errors = BaseValidate(input) ?? new List<string>();
            if (!errors.Any())
                return (false, errors);

            return (!errors.Any(), errors);
        }
    }
}