using Microsoft.EntityFrameworkCore;
using Mulkora.DataAccess.Abstract;
using Mulkora.DataAccess.Concrete;
using Mulkora.DataAccess.Repository;
using Mulkora.Entity.Concrete;

namespace Mulkora.DataAccess.EntityFramework;

public class EfPropertyDal : GenericRepository<Property>, IPropertyDal
{
    private readonly Context _context;
    public EfPropertyDal(Context context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Property>> GetPropertiesByUserIdAsync(string userId)
    {
        var values = await _context.Properties
            .Include(x=>x.PropertyImages)
            .Where(x => x.Agent.AppUserId == userId)
            .ToListAsync();
        return values;
    }

    public async Task<List<Property>> GetPropertiesWithFeaturesAsync()
    {
        return await _context.Properties
            .Include(x => x.Features)
            .OrderByDescending(x => x.CreatedDate)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task InsertWithFeaturesAsync(Property property, List<int> featureIds)
    {
        var selectedFeatureIds = featureIds.Distinct().ToList();
        
        var features = await _context.Features.Where(x => selectedFeatureIds.Contains(x.FeatureId)).ToListAsync();

        foreach (var feature in features)
        {
            property.Features.Add(feature);
        }
        
        await _context.Properties.AddAsync(property);
        await _context.SaveChangesAsync();
    }
    
    //Distinct() tekrar eden ID’yi kaldırır:
    public async Task UpdateWithFeaturesAsync(Property property, List<int> featureIds)
    {
        var selectedFeatureIds = featureIds?
            .Distinct()
            .ToList() ?? new List<int>();

        var features = await _context.Features
            .Where(x => selectedFeatureIds.Contains(x.FeatureId))
            .ToListAsync();

        property.Features.Clear();

        foreach (var feature in features)
        {
            property.Features.Add(feature);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<Property> GetByIdWithFeaturesAsync(int id)
    {
        return await _context.Properties
            .Where(x => x.PropertyId == id)
            .Include(x => x.Features)
            .FirstOrDefaultAsync();
    }
    
    
}