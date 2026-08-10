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

    public async Task<List<Agent>> GetListAgentTrue()
    {
        return await _context.Agents
            .Where(x => x.IsActive == true && x.IsVerified == true)
            .Include(x => x.AppUser)
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedDate)
            .ToListAsync();
    }
    
    public async Task<Agent?> GetByUserIdAsync(string id)
    {
        var value = await _context.Agents
            .Include(x => x.AppUser)
            .FirstOrDefaultAsync(x => x.AppUserId == id);

        return value;
    }

    public async Task<List<Agent>> GetFilterAgent(string? text, bool? isTrue)
    {
        var query = _context.Agents
            .Include(x => x.AppUser)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(text))
        {
            query = query.Where(x =>
                x.City.Contains(text) ||
                x.AppUser.Name.Contains(text) ||
                x.AppUser.Surname.Contains(text) ||
                x.AppUser.Email.Contains(text));
        }

        if (isTrue != null)
        {
            query = query.Where(x=>x.IsActive == isTrue);
        }
        
        return await query.ToListAsync();
    }
}