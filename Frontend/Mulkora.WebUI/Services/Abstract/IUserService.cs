using Mulkora.Dto.UserDtos;

namespace Mulkora.WebUI.Services.Abstract;

public interface IUserService : IGenericService<ResultUserDto, CreateUserDto, UpdateUserDto>
{
    Task<RoleAssignDto> GetUserRolesAsync(string userId, string token);
    Task UpdateUserRolesAsync(RoleAssignDto dto, string token);
    Task<ResultUserDto> GetUserByIdAsync(string id, string token);
    Task<List<ResultUserDto>> GetUsers(string token);
}