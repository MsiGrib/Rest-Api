using DataModel.Models.DataBase;
using EntityGateWay.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EntityGateWay.Repository.Implementations
{
    internal class UserRepository : IUserRepository
    {
        private readonly BonchiDBContext _context;

        public UserRepository(BonchiDBContext context)
        {
            _context = context;
        }

        #region Get

        public async Task<List<User>?> GetAllAsync()
        {
            return await _context.Users
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        #endregion

        #region Add

        public async Task AddAsync(User entity)
        {
            await _context.Users
                .AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Guid> RegistrationAsync(User entity)
        {
            await _context.Users
                .AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        #endregion

        #region Update

        public async Task UpdateAsync(User entity)
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
                _context.Users
                    .Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        #endregion

        #region Other

        public async Task<bool> IsExistsUserAsync(string email)
        {
            return await _context.Users
                .AnyAsync(x => x.Email == email);
        }

        #endregion
    }
}