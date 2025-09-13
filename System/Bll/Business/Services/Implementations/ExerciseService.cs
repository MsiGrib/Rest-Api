using Business.Mappers;
using Business.Models.Exercise.Dto;
using Business.Models.Exercise.Input;
using Business.Services.Interfaces;
using DataModel.Models;
using DataModel.Models.DataBase;
using EntityGateWay.Repository.Interfaces;

namespace Business.Services.Implementations
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepository _repository;

        public ExerciseService(IExerciseRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ExerciseDto>?> GetAllAsync(ExerciseFilterInput? input = null)
        {
            List<Exercise>? exercises = null;

            if (input is null)
                exercises = await _repository.GetAllAsync();
            else
                exercises = await _repository.GetAllForPaginationFilterAsync(new Pagination
                {
                    UsePagination = input.UsePagination,
                    PageNumber = input.PageNumber,
                    PageSize = input.PageSize,
                }, input.ExerciseId, input.Name, input.SetsFrom, input.SetsTo, input.RepsFrom, input.RepsTo, input.WeightFrom, input.WeightTo);

            return ExerciseMapper.ToDtoList(exercises);
        }
        public async Task<ExerciseDto?> GetByIdAsync(Guid id)
        {
            var exercise = await _repository.GetByIdAsync(id);

            return ExerciseMapper.ToDto(exercise);
        }

        public async Task<bool> AddAsync(ExerciseInput input)
        {
            var exercise = ExerciseMapper.ToDBEntity(input);
            if (exercise is null)
                return false;

            await _repository.AddAsync(exercise);

            return true;
        }

        public async Task<bool> UpdateExerciseAsync(UpdateExerciseInput input)
        {
            var exercise = await _repository.GetByIdAsync(input.Id);
            if (exercise is null)
                return false;

            var newExercise = exercise! with
            {
                Name = input.Name,
                Sets = input.Sets,
                Reps = input.Reps,
                Weight = input.Weight,
            };

            await _repository.UpdateAsync(newExercise);

            return true;
        }

        public async Task<bool> DeleteExerciseAsync(Guid id)
        {
            var exercise = await GetByIdAsync(id);
            if (exercise is null)
                return false;

            await _repository.DeleteByIdAsync(id);
            return true;
        }
    }
}