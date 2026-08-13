using Mulkora.Entity.Concrete;

namespace Mulkora.DataAccess.Abstract;

public interface IPropertyImageDal : IGenericDal<PropertyImage>
{
    Task InsertRangeAsync(List<PropertyImage> propertyImages);
    Task<List<PropertyImage>> GetImagesByPropertyIdAsync(int propertyId);
}