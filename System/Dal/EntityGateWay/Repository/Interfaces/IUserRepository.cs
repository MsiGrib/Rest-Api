using DataModel.Models.DataBase;

namespace EntityGateWay.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        public Task<bool> IsExistsUserAsync(string email);
        public Task<bool> IsExistsUserAsync(Guid id);
        public Task<Guid> RegistrationAsync(User entity);
        public Task<User?> GetByEmailAsync(string email);
    }
}