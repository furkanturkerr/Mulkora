using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mulkora.Business.Abstract;
using Mulkora.Dto.UserDtos;
using Mulkora.Entity.Concrete;

namespace Mulkora.Business.Manager;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    private readonly RoleManager<AppRole> _roleManager;

    public UserService(UserManager<AppUser> userManager, IMapper mapper, RoleManager<AppRole> roleManager)
    {
        _userManager = userManager;
        _mapper = mapper;
        _roleManager = roleManager;
    }

    public async Task<List<ResultUserDto>> TGetListAsync()
    {
        var values = await _userManager.Users.ToListAsync();
        return _mapper.Map<List<ResultUserDto>>(values);
    }

    public Task<UpdateUserDto> TGetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task TInsertAsync(CreateUserDto dto)
    {
        throw new NotImplementedException();
    }

    public Task TUpdateAsync(UpdateUserDto dto)
    {
        throw new NotImplementedException();
    }

    public Task TDeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<RoleAssignDto> GetUserRolesAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            throw new Exception("Kullanıcı bulunamadı.");

        var userRoles = await _userManager.GetRolesAsync(user);
        
        var roles = await _roleManager.Roles.ToListAsync();
        
        var dto = new RoleAssignDto
        {
            Id = user.Id,
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email ?? string.Empty,

            Roles = roles.Select(role => new RoleAssignItemDto
            {
                RoleId = role.Id,
                RoleName = role.Name ?? string.Empty,
                IsSelected = userRoles.Contains(role.Name!)
            }).ToList()
        };

        return dto;
    }

    public async Task UpdateUserRolesAsync(RoleAssignDto dto)
    {
        var user = await _userManager.FindByIdAsync(dto.Id);

        if (user == null)
            throw new Exception("Kullanıcı bulunamadı.");

        var currentRoles = await _userManager.GetRolesAsync(user);

        var selectedRoles = dto.Roles
            .Where(x => x.IsSelected)
            .Select(x => x.RoleName)
            .ToList();

        var rolesToRemove = currentRoles
            .Except(selectedRoles)
            .ToList();

        var rolesToAdd = selectedRoles
            .Except(currentRoles)
            .ToList();

        if (rolesToRemove.Any())
        {
            var removeResult = await _userManager.RemoveFromRolesAsync(
                user,
                rolesToRemove);

            if (!removeResult.Succeeded)
                throw new Exception("Kullanıcı rolleri kaldırılamadı.");
        }

        if (rolesToAdd.Any())
        {
            var addResult = await _userManager.AddToRolesAsync(
                user,
                rolesToAdd);

            if (!addResult.Succeeded)
                throw new Exception("Kullanıcı rolleri eklenemedi.");
        }
    }
}