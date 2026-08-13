using Mulkora.Dto.PropertyImageDtos;

namespace Mulkora.WebUI.Services.Abstract;

public interface IPropertyImageService
{
    Task<List<UpdatePropertyImageDto>> GetImagesByPropertyIdAsync(int propertyId, string token);
    Task UploadPropertyImagesAsync(int propertyId, List<IFormFile> images, string token);
}