using Mulkora.Dto.UserDtos;

namespace Mulkora.WebUI.Services.Abstract;

public interface IUserService : IGenericService<ResultUserDto, CreateUserDto, UpdateUserDto>
{
    Task<RoleAssignDto> GetUserRolesAsync(string userId);
    Task UpdateUserRolesAsync(RoleAssignDto dto);
}