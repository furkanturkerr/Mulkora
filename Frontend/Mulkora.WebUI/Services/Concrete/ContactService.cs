using Mulkora.Dto.ContactDtos;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Services.Concrete;

public class ContactService : GenericService<ResultContactDto, CreateContactDto, UpdateContactDto>, IContactService
{
    private readonly HttpClient _client;
    public ContactService(IHttpClientFactory httpClientFactory, HttpClient client) : base(httpClientFactory)
    {
        _client = client;
    }

    protected override string ApiRoute => "api/Contacts";
    public async Task<List<ResultContactDto>> GetFullListAsync(int page)
    {
        var response = await _client.GetAsync($"http://localhost:5214/api/Contacts?page={page}");
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<List<ResultContactDto>>() ?? [];
    }
}