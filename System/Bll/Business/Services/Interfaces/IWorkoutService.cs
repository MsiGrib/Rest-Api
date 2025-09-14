using Business.Models.Workout.Dto;
using Business.Models.Workout.Input;

namespace Business.Services.Interfaces
{
    public interface IWorkoutService
    {
        public Task<List<WorkoutDto>?> GetAllByUserIdAsync(Guid userId);
        public Task<WorkoutDto?> StartWorkoutAsync(WorkoutInput input, Guid userId);
        public Task<WorkoutDto?> UpdateWorkoutAsync(UpdateWorkoutInput input, Guid userId);
        public Task<bool> DeleteWorkoutAsync(Guid id);
    }
}