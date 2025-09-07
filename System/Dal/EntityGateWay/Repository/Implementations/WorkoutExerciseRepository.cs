using DataModel.Models.DataBase;
using EntityGateWay.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EntityGateWay.Repository.Implementations
{
    internal class WorkoutExerciseRepository : IWorkoutExerciseRepository
    {
        private readonly BonchiDBContext _context;

        public WorkoutExerciseRepository(BonchiDBContext context)
        {
            _context = context;
        }

        #region Get

        public async Task<List<WorkoutExercise>?> GetAllAsync()
        {
            return await _context.WorkoutsExercises
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<WorkoutExercise?> GetByIdAsync(long id)
        {
            return await _context.WorkoutsExercises
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        #endregion

        #region Add

        public async Task AddAsync(WorkoutExercise entity)
        {
            await _context.WorkoutsExercises
                .AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region Update

        public async Task UpdateAsync(WorkoutExercise entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region Delete

        public async Task DeleteByIdAsync(long id)
        {
            var entity = await GetByIdAsync(id);
            if (entity is not null)
            {
                _context.WorkoutsExercises
                    .Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        #endregion
    }
}