using Microsoft.EntityFrameworkCore;
using Mulkora.DataAccess.Abstract;
using Mulkora.DataAccess.Concrete;
using Mulkora.DataAccess.Repository;
using Mulkora.Entity.Concrete;

namespace Mulkora.DataAccess.EntityFramework;

public class EfFeatureDal : GenericRepository<Feature>, IFeatureDal
{
    private readonly Context _context;
    public EfFeatureDal(Context context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Feature>> GetFeatureByPropertyIdAsync(List<int> propertyIds)
    {
        //“Bu değer, ids listesinin içinde var mı?”
        return await _context.Features
            .Where(x => propertyIds.Contains(x.FeatureId))
            .ToListAsync();
    }
}