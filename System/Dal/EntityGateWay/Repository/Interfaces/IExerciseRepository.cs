using DataModel.Models;
using DataModel.Models.DataBase;

namespace EntityGateWay.Repository.Interfaces
{
    public interface IExerciseRepository : IRepository<Exercise, Guid>
    {
        public Task<List<Exercise>?> GetAllForPaginationFilterAsync(Pagination pagination,
            Guid? id = null, string? name = null, int? setsFrom = null, int? setsTo = null,
            int? repsFrom = null, int? repsTo = null, double? weightFrom = null,
            double? weightTo = null);
    }
}