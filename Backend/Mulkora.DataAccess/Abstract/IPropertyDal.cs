using Mulkora.Entity.Concrete;
using Mulkora.Entity.Enums;

namespace Mulkora.DataAccess.Abstract;

public interface IPropertyDal : IGenericDal<Property>
{ 
    Task<List<Property>> GetPropertiesByUserIdAsync(string userId, string? text, PropertyStatus? IsStatus);
    Task<List<Property>> GetPropertiesWithFeaturesAsync();
    Task InsertWithFeaturesAsync(Property property, List<int> featureIds);
    Task UpdateWithFeaturesAsync(Property property, List<int> featureIds);
    Task<Property> GetByIdWithFeaturesAsync(int id);
    Task<List<Property>> GetFilterProperty(string? text, PropertyStatus? IsStatus, string? City, string? District, ListingType? ListingType, int page, int pageSize);
    Task<int> GetFilterPropertyCount(string? text, PropertyStatus? IsStatus, string? City, string? District, ListingType? ListingType);
    Task<List<Property>> GetFilterPropertyAll(string? city, string? district, ListingType? listingType, int? maxPrice, int? minPrice, int? categoryId, int? roomCount, int page, int pageSize);
    Task<int> GetFilterPropertyAllCount(string? city, string? district, ListingType? listingType, int? maxPrice, int? minPrice, int? categoryId, int? roomCount);
    Task<Property?> GetPublishedByIdWithFeaturesAsync(int id);
}