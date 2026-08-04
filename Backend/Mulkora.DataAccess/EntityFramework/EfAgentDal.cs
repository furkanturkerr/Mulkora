using Microsoft.EntityFrameworkCore;
using Mulkora.DataAccess.Abstract;
using Mulkora.DataAccess.Concrete;
using Mulkora.DataAccess.Repository;
using Mulkora.Entity.Concrete;

namespace Mulkora.DataAccess.EntityFramework;

public class EfAgentDal : GenericRepository<Agent>, IAgentDal
{
    private readonly Context _context;
    
    public EfAgentDal(Context context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Agent>> GetAllWithUserAsync()
    {
        return await _context.Agents
            .Include(x => x.AppUser)
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedDate)
            .ToListAsync();
    }

    public async Task<Agent> GetWithUserByIdAsync(int id)
    {
        var value = await _context.Agents
            .Where(x=>x.AgentId == id)
            .Include(x => x.AppUser)
            .FirstOrDefaultAsync();
        return value;
    }
}