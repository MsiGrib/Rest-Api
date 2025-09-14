using Business.Mappers;
using Business.Models.Workout.Dto;
using Business.Models.Workout.Input;
using Business.Services.Interfaces;
using DataModel.Models.DataBase;
using EntityGateWay.Repository.Interfaces;

namespace Business.Services.Implementations
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IWorkoutRepository _workoutRepository;
        private readonly IWorkoutExerciseRepository _workoutExerciseRepository;

        public WorkoutService(IWorkoutRepository workoutRepository, IWorkoutExerciseRepository workoutExerciseRepository)
        {
            _workoutRepository = workoutRepository;
            _workoutExerciseRepository = workoutExerciseRepository;
        }

        public async Task<List<WorkoutDto>?> GetAllByUserIdAsync(Guid userId)
        {
            var workouts = await _workoutRepository.GetAllByUserIdAsync(userId);

            return WorkoutMapper.ToDtoList(workouts);
        }

        public async Task<WorkoutDto?> StartWorkoutAsync(WorkoutInput input, Guid userId)
        {
            var newWorkout = WorkoutMapper.ToDBEntity(input, userId);
            if (newWorkout is null)
                return null;

            var workout = await _workoutRepository.StartWorkoutAsync(newWorkout);

            foreach (var item in input.ExerciseIds ?? new List<Guid>())
            {
                if (!await _workoutExerciseRepository.IsExistWorkoutExerciseByWorkoutExerciseId(workout.Id, item))
                {
                    await _workoutExerciseRepository.AddAsync(new WorkoutExercise
                    {
                        WorkoutId = workout.Id,
                        ExerciseId = item,
                    });
                }
            }
            
            return WorkoutMapper.ToDto(workout);
        }

        public async Task<WorkoutDto?> UpdateWorkoutAsync(UpdateWorkoutInput input, Guid userId)
        {
            var workout = await _workoutRepository.GetByIdAsync(input.Id);
            if (workout is null)
                return null;

            var newWorkout = workout! with
            {
                StartDate = input.StartDate,
                EndDate = input.EndDate,
                Notes = input.Notes,
            };

            await _workoutRepository.UpdateAsync(newWorkout);

            foreach (var item in input.ExerciseIds ?? new List<Guid>())
            {
                if (!await _workoutExerciseRepository.IsExistWorkoutExerciseByWorkoutExerciseId(workout.Id, item))
                {
                    await _workoutExerciseRepository.AddAsync(new WorkoutExercise
                    {
                        WorkoutId = workout.Id,
                        ExerciseId = item,
                    });
                }
            }

            return WorkoutMapper.ToDto(newWorkout);
        }

        public async Task<bool> DeleteWorkoutAsync(Guid id)
        {
            var workout = await _workoutRepository.GetByIdAsync(id);

            if (workout is not null)
            {
                var newWorkout = workout! with
                {
                    DeleteTime = DateTime.UtcNow,
                };
                await _workoutRepository.UpdateAsync(newWorkout);

                return true;
            }

            return false;
        }
    }
}