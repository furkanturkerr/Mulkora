using System.Net.Http.Headers;
using System.Net.Http.Json;
using Mulkora.Dto.AppointmentDtos;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Services.Concrete;

public class AppointmentService : GenericService<ResultAppointmentDto, CreateAppointmentDto, UpdateAppointmentDto>, IAppointmentService
{
    private readonly HttpClient _client;

    public AppointmentService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
        _client = httpClientFactory.CreateClient("MulkoraApi");
    }

    protected override string ApiRoute => "api/Appointment";

    public async Task<HttpResponseMessage> CreateAsync(CreateAppointmentDto dto, string token)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, ApiRoute);

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        request.Content = JsonContent.Create(dto);

        return await _client.SendAsync(request);
    }

    public async Task<List<ResultAppointmentDto>> GetAppointmentsByUserIdAsync(string userId, string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var response = await _client.GetAsync($"{ApiRoute}/user");
        
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<List<ResultAppointmentDto>>() ?? new List<ResultAppointmentDto>();
    }

    public async Task<List<ResultAppointmentDto>> GetAppointmentsByAgentUserIdAsync(string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var response = await _client.GetAsync($"{ApiRoute}/agent");
        
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<List<ResultAppointmentDto>>() ?? new List<ResultAppointmentDto>();
    }

    public async Task ApproveAsync(int id, string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var request = new HttpRequestMessage(HttpMethod.Patch, $"{ApiRoute}/{id}/approve");
        var response = await _client.SendAsync(request);
    }
}