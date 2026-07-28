using Otelvexa.WebUI.Services.Abstract;

namespace Otelvexa.WebUI.Services.Concrete;

public abstract class GenericService<TResultDto, TCreateDto, TUpdateDto> : IGenericService<TResultDto, TCreateDto, TUpdateDto>
{
    private readonly HttpClient _client;

    protected abstract string ApiRoute { get; }

    protected GenericService(IHttpClientFactory httpClientFactory)
    {
        _client = httpClientFactory.CreateClient("OtelvexaApi");
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

    public async Task TInsertAsync(TCreateDto dto)
    {
        var response = await _client.PostAsJsonAsync(ApiRoute, dto);
        response.EnsureSuccessStatusCode();
    }

    public async Task TUpdateAsync(TUpdateDto dto)
    {
        var response = await _client.PutAsJsonAsync(ApiRoute, dto);
        response.EnsureSuccessStatusCode();
    }

    public async Task TDeleteAsync(int id)
    {
        var response = await _client.DeleteAsync($"{ApiRoute}/{id}");
        response.EnsureSuccessStatusCode();
    }
}