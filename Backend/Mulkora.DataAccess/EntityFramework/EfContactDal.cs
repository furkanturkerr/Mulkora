using Microsoft.EntityFrameworkCore;
using Mulkora.DataAccess.Abstract;
using Mulkora.DataAccess.Concrete;
using Mulkora.DataAccess.Repository;
using Mulkora.Entity.Concrete;

namespace Mulkora.DataAccess.EntityFramework;

public class EfContactDal : GenericRepository<Contact>, IContactDal
{
    private readonly Context _context;
    public EfContactDal(Context context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Contact>> GetFullListAsync(int page)
    {
        if (page < 1)
            page = 1;

        int pagesize = 5;

        int skip = (page - 1) * pagesize;

        return await _context.Contacts.Skip(skip).Take(pagesize).ToListAsync();
    }
}