using DataModel.Models.DataBase;
using EntityGateWay.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EntityGateWay.Repository.Implementations
{
    internal class WorkoutRepository : IWorkoutRepository
    {
        private readonly BonchiDBContext _context;

        public WorkoutRepository(BonchiDBContext context)
        {
            _context = context;
        }

        #region Get

        public async Task<List<Workout>?> GetAllAsync()
        {
            return await _context.Workouts
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Workout?> GetByIdAsync(Guid id)
        {
            return await _context.Workouts
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        #endregion

        #region Add

        public async Task AddAsync(Workout entity)
        {
            await _context.Workouts
                .AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region Update

        public async Task UpdateAsync(Workout entity)
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
                _context.Workouts
                    .Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        #endregion
    }
}