using System.Net.Http.Headers;
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
    
    public async Task<RoleAssignDto> GetUserRolesAsync(string userId, string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _client.GetAsync($"{ApiRoute}/{userId}/roles");
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<RoleAssignDto>();
    }

    public async Task UpdateUserRolesAsync(RoleAssignDto dto, string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _client.PutAsJsonAsync(
            $"{ApiRoute}/roles",
            dto);
        response.EnsureSuccessStatusCode();       
    }

    public async Task<ResultUserDto> GetUserByIdAsync(string id, string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var values = await _client.GetAsync($"{ApiRoute}/{id}");
        values.EnsureSuccessStatusCode();
        
        return await values.Content.ReadFromJsonAsync<ResultUserDto>();
    }

    public async Task<List<ResultUserDto>> GetUsers(string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var values = await _client.GetAsync($"{ApiRoute}");
        values.EnsureSuccessStatusCode();
        
        return await values.Content.ReadFromJsonAsync<List<ResultUserDto>>();
    }
}