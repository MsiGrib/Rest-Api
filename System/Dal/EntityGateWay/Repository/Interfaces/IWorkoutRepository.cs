using DataModel.Models.DataBase;

namespace EntityGateWay.Repository.Interfaces
{
    public interface IWorkoutRepository : IRepository<Workout, Guid>
    {
        public Task<List<Workout>?> GetAllByUserIdAsync(Guid userId);
        public Task<Workout> StartWorkoutAsync(Workout entity);
    }
}