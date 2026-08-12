using Mulkora.Entity.Concrete;

namespace Mulkora.DataAccess.Abstract;

public interface IPropertyDal : IGenericDal<Property>
{ 
    Task<List<Property>> GetPropertiesByUserIdAsync(string userId);
    Task<List<Property>> GetPropertiesWithFeaturesAsync();
}