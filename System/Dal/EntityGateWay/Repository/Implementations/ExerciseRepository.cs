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