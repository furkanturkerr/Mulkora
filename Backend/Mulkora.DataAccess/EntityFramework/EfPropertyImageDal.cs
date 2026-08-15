using Microsoft.EntityFrameworkCore;
using Mulkora.DataAccess.Abstract;
using Mulkora.DataAccess.Concrete;
using Mulkora.DataAccess.Repository;
using Mulkora.Entity.Concrete;
using Mulkora.Entity.Enums;

namespace Mulkora.DataAccess.EntityFramework;

public class EfPropertyImageDal : GenericRepository<PropertyImage>, IPropertyImageDal
{
    private readonly Context _context;

    public EfPropertyImageDal(Context context) : base(context)
    {
        _context = context;
    }

    public async Task InsertRangeAsync(List<PropertyImage> propertyImages)
    {
        await _context.PropertyImages.AddRangeAsync(propertyImages);
        //Tek kayır değil o yüzden AddRangeAsync

        await _context.SaveChangesAsync();
    }

    public async Task<List<PropertyImage>> GetImagesByPropertyIdAsync(int propertyId)
    {
        var values = await _context.PropertyImages
            .Where(x => x.PropertyId == propertyId)
            .ToListAsync();
        
        return values;
    }
    
    public async Task<PropertyImage?> GetByIdWithPropertyAsync(int imageId)
    {
        return await _context.PropertyImages
            .Include(x => x.Property)
            .FirstOrDefaultAsync(x => x.PropertyImageId == imageId);
    }
    
    public async Task<List<PropertyImage>> GetPublicImagesByPropertyIdAsync(int propertyId)
    {
        return await _context.PropertyImages
            .Where(x => x.PropertyId == propertyId && x.Property.Status == PropertyStatus.Published)
            .OrderBy(x => x.DisplayOrder)
            .ThenBy(x => x.PropertyImageId)
            .AsNoTracking()
            .ToListAsync();
    }
}