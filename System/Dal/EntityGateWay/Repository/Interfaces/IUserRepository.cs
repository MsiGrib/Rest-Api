using DataModel.Enums;
using DataModel.Models;
using DataModel.Models.DataBase;

namespace EntityGateWay.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        public Task<List<User>?> GetAllForPaginationFilterAsync(Pagination pagination,
                Guid? id = null, string? name = null, string? email = null,
                string? numberPhone = null, UserType? type = null);
        public Task<bool> IsExistsUserAsync(string email);
        public Task<bool> IsExistsUserAsync(Guid id);
        public Task<Guid> RegistrationAsync(User entity);
        public Task<User?> GetByEmailAsync(string email);
    }
}