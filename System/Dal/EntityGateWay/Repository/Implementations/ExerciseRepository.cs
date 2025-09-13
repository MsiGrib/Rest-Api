using DataModel.Models;
using DataModel.Models.DataBase;
using EntityGateWay.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EntityGateWay.Repository.Implementations
{
    internal class ExerciseRepository : IExerciseRepository
    {
        private readonly BonchiDBContext _context;

        public ExerciseRepository(BonchiDBContext context)
        {
            _context = context;
        }

        #region Get

        public async Task<List<Exercise>?> GetAllAsync()
        {
            return await _context.Exercises
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Exercise?> GetByIdAsync(Guid id)
        {
            return await _context.Exercises
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Exercise>?> GetAllForPaginationFilterAsync(Pagination pagination,
            Guid? id = null, string? name = null, int? setsFrom = null, int? setsTo = null,
            int? repsFrom = null, int? repsTo = null, double? weightFrom = null,
            double? weightTo = null)
        { 
            var query = _context.Exercises
                    .AsNoTracking();

            if (id.HasValue)
                query = query.Where(x => x.Id == id.Value);
            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.Name == name);
            if (setsFrom.HasValue)
                query = query.Where(x => x.Sets >= setsFrom.Value);
            if (setsTo.HasValue)
                query = query.Where(x => x.Sets <= setsTo.Value);
            if (repsFrom.HasValue)
                query = query.Where(x => x.Reps >= repsFrom.Value);
            if (repsTo.HasValue)
                query = query.Where(x => x.Reps <= repsTo.Value);
            if (weightFrom.HasValue)
                query = query.Where(x => x.Weight >= weightFrom.Value);
            if (weightTo.HasValue)
                query = query.Where(x => x.Weight <= weightTo.Value);

            List<Exercise>? exercises = null;

            if (pagination.UsePagination)
                exercises = await query
                    .Skip(pagination.Skip)
                    .Take(pagination.PageSize)
                    .ToListAsync();
            else
                exercises = await query.ToListAsync();

            return exercises;
        }

        #endregion

        #region Add

        public async Task AddAsync(Exercise entity)
        {
            await _context.Exercises
                .AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region Update

        public async Task UpdateAsync(Exercise entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region Delete

        public async Task DeleteByIdAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity is not null)
            {
                _context.Exercises
                    .Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        #endregion
    }
}