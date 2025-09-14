using Business.Models.Exercise.Dto;
using Business.Models.Exercise.Input;
using DataModel.Models.DataBase;

namespace Business.Mappers
{
    public static class ExerciseMapper
    {
        public static ExerciseDto? ToDto(Exercise? exercise)
        {
            if (exercise is null)
                return null;

            return new ExerciseDto
            {
                Id = exercise.Id,
                Name = exercise.Name,
                Sets = exercise.Sets,
                Reps = exercise.Reps,
                Weight = exercise.Weight,
            };
        }

        public static Exercise? ToDBEntity(ExerciseInput? input)
        {
            if (input is null)
                return null;

            return new Exercise
            {
                Name = input.Name,
                Sets = input.Sets,
                Reps = input.Reps,
                Weight = input.Weight,
                DeleteTime = null,
            };
        }

        public static List<ExerciseDto>? ToDtoList(IEnumerable<Exercise>? exercises)
        {
            if (exercises is null)
                return null;

            return exercises
                .Where(e => e != null)
                .Select(ToDto)!
                .Where(dto => dto != null)
                .ToList()!;
        }
    }
}