using DataModel.Enums;
using DataModel.Models;

namespace Business.Models.User.Input
{
    public class UserFilterInput : Pagination
    {
        public Guid? UserId { get; init; } = null;
        public string? Name { get; init; } = null;
        public string? Email { get; init; } = null;
        public string? NumberPhone { get; init; } = null;
        public UserType? UserType { get; init; } = null;
    }
}