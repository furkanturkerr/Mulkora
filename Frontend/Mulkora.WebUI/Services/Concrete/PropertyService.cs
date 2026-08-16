using System.Net;
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
    
    public async Task<List<ResultPropertyDto>> GetPropertiesByUserIdAsync(string token, string? text, int? IsStatus)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.GetAsync($"{ApiRoute}/my-properties?text={text}&IsStatus={IsStatus}");

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

    public async Task<List<ResultPropertyDto>> GetFilterProperty(string? text, int? IsStatus, string? City, string? District, int? ListingType, int page, int pageSize,string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var response = await _client.GetAsync( $"{ApiRoute}/filter?text={text}&IsStatus={IsStatus}&City={City}&District={District}&ListingType={ListingType}&page={page}&pageSize={pageSize}");
        
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<List<ResultPropertyDto>>() ?? [];
    }

    public async Task<List<ResultPropertyDto>> GetFilterPropertyAll(string? city, string? district, int? listingType, int? maxPrice, int? minPrice,
        int? categoryId, int? roomCount, int page, int pageSize)
    {
        var response = await _client.GetAsync(
            $"http://localhost:5214/api/Properties/filterAll?city={city}&district={district}&listingType={listingType}&maxPrice={maxPrice}&minPrice={minPrice}&categoryId={categoryId}&roomCount={roomCount}&page={page}&pageSize={pageSize}");

        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<List<ResultPropertyDto>>() ?? [];
    }
    
    public async Task<GetByIdPropertyDto?> GetPublishedByIdAsync(int id)
    {
        var response = await _client.GetAsync($"{ApiRoute}/published/{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();

            throw new Exception($"API Hatası: {(int)response.StatusCode} - {errorMessage}");
        }

        return await response.Content.ReadFromJsonAsync<GetByIdPropertyDto>();
    }
    
    public async Task<HttpResponseMessage> MarkAsSoldAsync(int id, string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var request = new HttpRequestMessage(HttpMethod.Patch, $"{ApiRoute}/{id}/mark-as-sold");
        
        return await _client.SendAsync(request);      
    }

    public async Task<HttpResponseMessage> MarkAsRentedAsync(int id, string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var request = new HttpRequestMessage(HttpMethod.Patch, $"{ApiRoute}/{id}/mark-as-rented");
        
        return await _client.SendAsync(request);     
    }
}