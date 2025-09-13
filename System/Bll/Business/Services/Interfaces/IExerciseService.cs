using Business.Models.Exercise.Dto;
using Business.Models.Exercise.Input;

namespace Business.Services.Interfaces
{
    public interface IExerciseService
    {
        public Task<List<ExerciseDto>?> GetAllAsync(ExerciseFilterInput? input = null);
        public Task<ExerciseDto?> GetByIdAsync(Guid id);
        public Task<bool> AddAsync(ExerciseInput input);
        public Task<bool> UpdateExerciseAsync(UpdateExerciseInput input);
        public Task<bool> DeleteExerciseAsync(Guid id);
    }
}