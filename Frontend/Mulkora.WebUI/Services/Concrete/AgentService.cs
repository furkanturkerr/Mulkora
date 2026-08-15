using System.Net.Http.Headers;
using Mulkora.Dto.AgentDtos;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Services.Concrete;

public class AgentService : GenericService<ResultAgentDto, CreateAgentDto, UpdateAgentDto>, IAgentService
{
    private readonly HttpClient _client;
    public AgentService(IHttpClientFactory httpClientFactory, HttpClient client) : base(httpClientFactory)
    {
        _client = httpClientFactory.CreateClient("MulkoraApi");
    }

    protected override string ApiRoute => "api/Agents";
    public async Task<List<ResultAgentDto>> GetListAgentTrue()
    {
        var response = await _client.GetAsync($"{ApiRoute}/true");
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<List<ResultAgentDto>>() ?? [];
    }

    public async Task<List<ResultAgentDto>> GetFilterAgent(string? text, bool? isTrue, string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
        var response = await _client.GetAsync($"{ApiRoute}/filter?text={text}&isTrue={isTrue}");
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<List<ResultAgentDto>>() ?? [];
    }
    
    public async Task<UpdateAgentDto?> GetAgentByIdAsync(int id)
    {
        var response = await _client.GetAsync($"{ApiRoute}/agent/{id}");
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<UpdateAgentDto>();
    }
}