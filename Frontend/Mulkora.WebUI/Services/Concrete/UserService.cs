using Mulkora.Dto.UserDtos;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Services.Concrete;

public class UserService : GenericService<ResultUserDto, CreateUserDto, UpdateUserDto>, IUserService
{
    private readonly HttpClient _client;
    public UserService(IHttpClientFactory httpClientFactory, HttpClient client) : base(httpClientFactory)
    {
        _client = httpClientFactory.CreateClient("MulkoraApi");
    }
    
    protected override string ApiRoute => "api/Users";
    
    public async Task<RoleAssignDto> GetUserRolesAsync(string userId)
    {
        var response = await _client.GetAsync($"{ApiRoute}/{userId}/roles");
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<RoleAssignDto>();
    }

    public async Task UpdateUserRolesAsync(RoleAssignDto dto)
    {
        var response = await _client.PutAsJsonAsync(
            $"{ApiRoute}/roles",
            dto);
        response.EnsureSuccessStatusCode();       
    }
}