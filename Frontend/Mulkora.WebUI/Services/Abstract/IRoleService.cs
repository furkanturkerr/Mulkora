using Mulkora.Dto.RoleDtos;

namespace Mulkora.WebUI.Services.Abstract;

public interface IRoleService : IGenericService<ResultRoleDto, CreateRoleDto, UpdateRoleDto>
{
    Task<UpdateRoleDto> GetRoleByIdAsync(string id);
    Task DeleteRoleAsync(string id);
}