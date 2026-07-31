using Microsoft.EntityFrameworkCore;
using Mulkora.DataAccess.Abstract;
using Mulkora.DataAccess.Concrete;

namespace Mulkora.DataAccess.Repository;

public class GenericRepository<T> : IGenericDal<T> where T : class 
{
    private readonly Context _context;

    public GenericRepository(Context context)
    {
        _context = context;
    }

    public async Task InsertAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    { 
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<List<T>> GetListAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }
}