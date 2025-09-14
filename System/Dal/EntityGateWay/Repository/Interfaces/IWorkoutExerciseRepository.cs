using DataModel.Models.DataBase;

namespace EntityGateWay.Repository.Interfaces
{
    public interface IWorkoutExerciseRepository : IRepository<WorkoutExercise, long>
    {
        public Task<bool> IsExistWorkoutExerciseByWorkoutExerciseId(Guid workoutId, Guid exerciseId);
    }
}