using Microsoft.AspNetCore.Identity;
using Mulkora.Entity.Concrete;

namespace Mulkora.Business.Abstract;

public interface IRoleService
{
    Task<List<AppRole>> GetAllRoles();
    Task<IdentityResult> CreateRoleAsync(string roleName);
    Task<IdentityResult> DeleteRoleAsync(string id);
    Task<IdentityResult> UpdateRoleAsync(string id, string roleName);
}