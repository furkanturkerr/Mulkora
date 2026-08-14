using System.Net.Http.Headers;
using Mulkora.Dto.PropertyImageDtos;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Services.Concrete;

public class PropertyImageService : GenericService<ResultPropertyImageDto, CreatePropertyImageDto, UpdatePropertyImageDto>, IPropertyImageService
{
    
    private readonly HttpClient _client;
    
    public PropertyImageService(IHttpClientFactory httpClientFactory, HttpClient client) : base(httpClientFactory)
    {
        _client = httpClientFactory.CreateClient("MulkoraApi");
    }

    protected override string ApiRoute => "api/PropertyImages";
    
    public async Task<List<UpdatePropertyImageDto>> GetImagesByPropertyIdAsync(int propertyId, string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var response = await _client.GetAsync($"{ApiRoute}/property/{propertyId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<UpdatePropertyImageDto>>() ?? [];
    }

    public async Task UploadPropertyImagesAsync(int propertyId, List<IFormFile> images, string token)
    {
        using var formContent = new MultipartFormDataContent();

        foreach (var image in images)
        {
            var fileContent = new StreamContent(image.OpenReadStream());

            if (!string.IsNullOrEmpty(image.ContentType))
            {
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(image.ContentType);
            }

            formContent.Add(fileContent, "images", image.FileName);
        }

        using var request = new HttpRequestMessage(HttpMethod.Post, $"{ApiRoute}/{propertyId}");

        request.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        request.Content = formContent;

        var response = await _client.SendAsync(request);

        response.EnsureSuccessStatusCode();
    } 
    
    public async Task DeleteImageAsync(int imageId, string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.DeleteAsync($"{ApiRoute}/{imageId}");

        response.EnsureSuccessStatusCode();
    }
}