using Mulkora.Entity.Concrete;

namespace Mulkora.DataAccess.Abstract;

public interface IContactDal : IGenericDal<Contact>
{
    Task<List<Contact>> GetFullListAsync(int page);
}