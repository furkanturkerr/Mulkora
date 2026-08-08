using Microsoft.AspNetCore.Identity;
using Mulkora.Dto.RoleDtos;
using Mulkora.Entity.Concrete;

namespace Mulkora.Business.Abstract;

public interface IRoleService
{
    Task<List<AppRole>> GetAllRoles();
    Task<IdentityResult> CreateRoleAsync(CreateRoleDto dto);
    Task<IdentityResult> DeleteRoleAsync(string id);
    Task<IdentityResult> UpdateRoleAsync(UpdateRoleDto dto);
    Task<AppRole> GetRoleByIdAsync(string id);
}