using Mulkora.Entity.Concrete;

namespace Mulkora.DataAccess.Abstract;

public interface IFeatureDal : IGenericDal<Feature>
{
    Task<List<Feature>> GetFeatureByPropertyIdAsync(List<int> propertyIds);
}