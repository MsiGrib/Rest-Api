using Business.Models.Workout.Dto;
using Business.Models.Workout.Input;
using DataModel.Models.DataBase;

namespace Business.Mappers
{
    public static class WorkoutMapper
    {
        public static WorkoutDto? ToDto(Workout? workout)
        {
            if (workout is null)
                return null;

            return new WorkoutDto
            {
                Id = workout.Id,
                StartDate = workout.StartDate,
                EndDate = workout.EndDate,
                Notes = workout.Notes,
                UserId = workout.UserId,
                User = UserMapper.ToDto(workout.User),
            };
        }

        public static Workout? ToDBEntity(WorkoutInput? input, Guid userId)
        {
            if (input is null)
                return null;

            return new Workout
            {
                StartDate = input.StartDate,
                EndDate = null,
                Notes = input.Notes,
                UserId = userId,
            };
        }

        public static List<WorkoutDto>? ToDtoList(IEnumerable<Workout>? workouts)
        {
            if (workouts is null)
                return null;

            return workouts
                .Where(w => w != null)
                .Select(ToDto)!
                .Where(dto => dto != null)
                .ToList()!;
        }
    }
}