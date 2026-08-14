using Mulkora.Dto.PropertyImageDtos;

namespace Mulkora.Business.Abstract;

public interface IPropertyImageService
{
    Task TCheckPropertyOwnerAsync(int propertyId, int agentId);

    Task TInsertImagesAsync(CreatePropertyImagesDto dto);
    
    Task<List<UpdatePropertyImageDto>> TGetImagesByPropertyIdAsync(int propertyId);
    
    Task<string> TDeleteImageAsync(int imageId, int agentId);
}