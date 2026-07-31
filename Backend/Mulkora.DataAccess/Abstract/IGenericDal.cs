namespace Mulkora.DataAccess.Abstract;

public interface IGenericDal<T> 
{
    Task InsertAsync (T entity);
    Task UpdateAsync (T entity);
    Task DeleteAsync (int id);
    Task<T> GetByIdAsync (int id);
    Task<List<T>> GetListAsync();
}