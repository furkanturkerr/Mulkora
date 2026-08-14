using System.Net.Http.Headers;
using Mulkora.Dto.PropertyDtos;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Services.Concrete;

public class PropertyService : GenericService<ResultPropertyDto, CreatePropertyDto, UpdatePropertyDto>, IPropertyService
{
    private readonly HttpClient _client;
    
    public PropertyService(IHttpClientFactory httpClientFactory, HttpClient client) : base(httpClientFactory)
    {
        _client = httpClientFactory.CreateClient("MulkoraApi");
    }
    
    protected override string ApiRoute => "api/Properties";
    
    public async Task<List<ResultPropertyDto>> GetPropertiesByUserIdAsync(string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.GetAsync($"{ApiRoute}/my-properties");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<List<ResultPropertyDto>>() ?? new List<ResultPropertyDto>();
    }
    
    public async Task<HttpResponseMessage> CreatePropertyAsync(CreatePropertyDto dto, string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await _client.PostAsJsonAsync(ApiRoute, dto);
    }
    
    public async Task SendForApprovalAsync(int id, string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var request = new HttpRequestMessage(HttpMethod.Patch, $"{ApiRoute}/{id}/send-for-approval");

        var response = await _client.SendAsync(request);

        response.EnsureSuccessStatusCode();
    }

    public async Task ApproveAsync(int id, string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var request = new HttpRequestMessage(HttpMethod.Patch, $"{ApiRoute}/{id}/approve");
        
       var response = await _client.SendAsync(request);
        
        response.EnsureSuccessStatusCode();       
    }

    public async Task RejectAsync(int id, string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var request = new HttpRequestMessage(HttpMethod.Patch, $"{ApiRoute}/{id}/reject");
        
        var response = await _client.SendAsync(request);
        
        response.EnsureSuccessStatusCode();      
    }

    public async Task<GetByIdPropertyDto> GetByIdAsync(int id)
    {
        var response = await _client.GetAsync($"{ApiRoute}/{id}");
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<GetByIdPropertyDto>();
    }

    public async Task<HttpResponseMessage> UpdatePropertyAsync(UpdatePropertyDto dto, string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await _client.PutAsJsonAsync(ApiRoute, dto);
    }
}