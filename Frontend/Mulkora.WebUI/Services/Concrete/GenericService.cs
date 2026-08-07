using System.Net.Http.Json;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Services.Concrete;

public abstract class GenericService<TResultDto, TCreateDto, TUpdateDto> : IGenericService<TResultDto, TCreateDto, TUpdateDto>
{
    private readonly HttpClient _client;

    protected abstract string ApiRoute { get; }

    protected GenericService(IHttpClientFactory httpClientFactory)
    {
        _client = httpClientFactory.CreateClient("MulkoraApi");
    }

    public async Task<List<TResultDto>> GetAllAsync()
    {
        var response = await _client.GetAsync(ApiRoute);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<List<TResultDto>>() ?? [];
    }

    public async Task<TUpdateDto?> TGetByIdAsync(int id)
    {
        var response = await _client.GetAsync($"{ApiRoute}/{id}");
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<TUpdateDto>();
    }

    public async Task<HttpResponseMessage> TInsertAsync(TCreateDto dto)
    {
        return await _client.PostAsJsonAsync(ApiRoute, dto);
    }

    public async Task<HttpResponseMessage> TUpdateAsync(TUpdateDto dto)
    {
        return await _client.PutAsJsonAsync(ApiRoute, dto);
    }

    public async Task<HttpResponseMessage> TDeleteAsync(int id)
    {
        return await _client.DeleteAsync($"{ApiRoute}/{id}");
    }
}