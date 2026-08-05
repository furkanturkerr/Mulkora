using Mulkora.Dto.AgentDtos;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Services.Concrete;

public class AgentService : GenericService<ResultAgentDto, CreateAgentDto, UpdateAgentDto>, IAgentService
{
    private readonly HttpClient _client;
    public AgentService(IHttpClientFactory httpClientFactory, HttpClient client) : base(httpClientFactory)
    {
        _client = client;
    }

    protected override string ApiRoute => "api/Agents";
    public async Task<List<ResultAgentDto>> GetListAgentTrue()
    {
        var response = await _client.GetAsync("http://localhost:5214/api/Agents/true");
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<List<ResultAgentDto>>() ?? [];
    }

    public async Task<List<ResultAgentDto>> GetFilterAgent(string? text, bool? isTrue)
    {
        var response = await _client.GetAsync($"http://localhost:5214/api/Agents/filter?text={text}&isTrue={isTrue}");
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<List<ResultAgentDto>>() ?? [];
    }
}