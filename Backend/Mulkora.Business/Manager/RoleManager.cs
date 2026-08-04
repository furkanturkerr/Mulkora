using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mulkora.Business.Abstract;
using Mulkora.Entity.Concrete;

namespace Mulkora.Business.Manager;

public class RoleManager : IRoleService
{
    private readonly RoleManager<AppRole> _roleManager;

    public RoleManager(RoleManager<AppRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<List<AppRole>> GetAllRoles()
    {
        var values = await _roleManager.Roles.ToListAsync();
        return values;
    }

    public async Task<IdentityResult> CreateRoleAsync(string roleName)
    {
        AppRole appRole = new AppRole
        {
            Name = roleName
        };
        
        var result = await _roleManager.CreateAsync(appRole);
        if (!result.Succeeded)
            return result;
        
        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteRoleAsync(string id)
    {
        var value = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == id);
        if (value == null)
            return IdentityResult.Failed();
        
        return await _roleManager.DeleteAsync(value);
    }

    public async Task<IdentityResult> UpdateRoleAsync(string id, string roleName)
    {
        var value = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == id);
        if (value == null)
            return IdentityResult.Failed();
        
        value.Name = roleName;
        return await _roleManager.UpdateAsync(value);
    }
}