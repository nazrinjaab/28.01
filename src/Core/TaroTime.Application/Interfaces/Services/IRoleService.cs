
using TaroTime.Domain.Entities;
using TaroTime.Domain.Enums;

namespace TaroTime.Application.Interfaces.Services
{
    public interface IRoleService
    {
        Task AssignRoleAsync(AppUser user, UserRole role);
        Task<IList<string>> GetUserRolesAsync(AppUser user);
    }
}
