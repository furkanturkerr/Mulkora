using Microsoft.EntityFrameworkCore;
using Mulkora.DataAccess.Abstract;
using Mulkora.DataAccess.Concrete;
using Mulkora.DataAccess.Repository;
using Mulkora.Entity.Concrete;
using Mulkora.Entity.Enums;

namespace Mulkora.DataAccess.EntityFramework;

public class EfPropertyDal : GenericRepository<Property>, IPropertyDal
{
    private readonly Context _context;
    public EfPropertyDal(Context context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Property>> GetPropertiesByUserIdAsync(string userId, string? text, PropertyStatus? IsStatus)
    {
        var query = _context.Properties
            .Include(x => x.PropertyImages)
            .Where(x => x.Agent.AppUserId == userId)
            .OrderByDescending(x => x.CreatedDate)
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(text))
        {
            query = query.Where(x => x.Title.Contains(text) || x.City.Contains(text) || x.District.Contains(text) || x.Address.Contains(text));
        }
        
        if (IsStatus.HasValue)
            query = query.Where(x => x.Status == IsStatus.Value);
        
        return await query.ToListAsync();
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
    
    public async Task<List<Property>> GetFilterProperty(string? text, PropertyStatus? IsStatus, string? City, string? District, ListingType? ListingType, int page, int pageSize)
    {
        var skip = (page - 1) * pageSize;
        var query = CreateFilterQuery(text, IsStatus, City, District, ListingType);

        return await query
            .Include(x => x.PropertyImages)
            .Include(x => x.Agent)
            .ThenInclude(x => x.AppUser)
            .OrderByDescending(x => x.CreatedDate)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();
    }
    
    public async Task<int> GetFilterPropertyCount(string? text, PropertyStatus? IsStatus, string? City, string? District, ListingType? ListingType)
    {
        var query = CreateFilterQuery(text, IsStatus, City, District, ListingType);

        return await query.CountAsync();
    }
    
    private IQueryable<Property> CreateFilterQuery(string? text, PropertyStatus? IsStatus, string? City, string? District, ListingType? ListingType)
    {
        IQueryable<Property> query = _context.Properties.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(text))
        {
            query = query.Where(x => x.Title.Contains(text));
        }

        if (IsStatus.HasValue)
        {
            query = query.Where(x => x.Status == IsStatus.Value);
        }

        if (!string.IsNullOrWhiteSpace(City))
        {
            query = query.Where(x => x.City.Contains(City));
        }

        if (!string.IsNullOrWhiteSpace(District))
        {
            query = query.Where(x => x.District.Contains(District));
        }

        if (ListingType.HasValue)
        {
            query = query.Where(x => x.ListingType == ListingType.Value);
        }

        return query;
    }
}