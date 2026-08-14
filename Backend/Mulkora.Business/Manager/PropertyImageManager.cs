using AutoMapper;
using Mulkora.Business.Abstract;
using Mulkora.DataAccess.Abstract;
using Mulkora.Dto.PropertyImageDtos;
using Mulkora.Entity.Concrete;

namespace Mulkora.Business.Manager;

public class PropertyImageManager : IPropertyImageService
{
    private readonly IPropertyImageDal _propertyImageDal;
    private readonly IPropertyDal _propertyDal;
    private readonly IMapper _mapper;

    public PropertyImageManager(IPropertyImageDal propertyImageDal, IPropertyDal propertyDal, IMapper mapper)
    {
        _propertyImageDal = propertyImageDal;
        _propertyDal = propertyDal;
        _mapper = mapper;
    }

    public async Task TCheckPropertyOwnerAsync(int propertyId, int agentId)
    {
        var property = await _propertyDal.GetByIdWithFeaturesAsync(propertyId);

        if (property == null)
        {
            throw new Exception("İlan bulunamadı.");
        }

        if (property.AgentId != agentId)
        {
            throw new Exception(
                "Bu ilana görsel ekleme yetkiniz bulunmuyor.");
        }
    }

    public async Task TInsertImagesAsync(CreatePropertyImagesDto dto)
    {
        var propertyImages = new List<PropertyImage>();

        for (var i = 0; i < dto.ImageUrls.Count; i++)
        {
            var propertyImage = new PropertyImage
            {
                PropertyId = dto.PropertyId,
                ImageUrl = dto.ImageUrls[i],
                // Sıralama 1'den başlar.
                DisplayOrder = i + 1
            };

            propertyImages.Add(propertyImage);
        }

        await _propertyImageDal.InsertRangeAsync(propertyImages);
    }

    public async Task<List<UpdatePropertyImageDto>> TGetImagesByPropertyIdAsync(int propertyId)
    {
        var values = await _propertyImageDal.GetImagesByPropertyIdAsync(propertyId);
        return _mapper.Map<List<UpdatePropertyImageDto>>(values);
    }

    public async Task<string> TDeleteImageAsync(int imageId, int agentId)
    {
        var image = await _propertyImageDal.GetByIdWithPropertyAsync(imageId);
        
        if (image == null)
        {
            throw new Exception("Görsel bulunamadı.");
        }

        if (image.Property.AgentId != agentId)
        {
            throw new Exception("Bu görseli silme yetkiniz bulunmuyor.");
        }

        var imageUrl = image.ImageUrl;

        await _propertyImageDal.DeleteAsync(imageId);

        return imageUrl;
    }
}