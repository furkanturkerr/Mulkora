using Mulkora.Dto.RoleDtos;

namespace Mulkora.WebUI.Services.Abstract;

public interface IRoleService : IGenericService<ResultRoleDto, CreateRoleDto, UpdateRoleDto>
{
    Task<UpdateRoleDto> GetRoleByIdAsync(string id, string token);
    Task DeleteRoleAsync(string id, string token);
    Task<List<ResultRoleDto>> GetRoles(string token);
}