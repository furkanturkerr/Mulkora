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
        var values = await _context.Properties.Where(x => x.Agent.AppUserId == userId).ToListAsync();
        return values;
    }
}