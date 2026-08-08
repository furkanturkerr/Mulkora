using Mulkora.Dto.RoleDtos;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Services.Concrete;

public class RoleService : GenericService<ResultRoleDto, CreateRoleDto, UpdateRoleDto>,IRoleService
{
    private readonly HttpClient _client;
    public RoleService(IHttpClientFactory httpClientFactory, HttpClient client) : base(httpClientFactory)
    {
        _client = httpClientFactory.CreateClient("MulkoraApi");
    }

    protected override string ApiRoute => "api/Roles";
    
    public async Task<UpdateRoleDto> GetRoleByIdAsync(string id)
    {
        var response = await _client.GetAsync($"{ApiRoute}/{id}");
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<UpdateRoleDto>();
    }

    public async Task DeleteRoleAsync(string id)
    {
        var response = await _client.DeleteAsync($"{ApiRoute}/{id}");
        response.EnsureSuccessStatusCode();
    }
}