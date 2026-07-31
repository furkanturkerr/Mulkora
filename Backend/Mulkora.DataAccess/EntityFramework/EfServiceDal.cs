using Mulkora.DataAccess.Abstract;
using Mulkora.DataAccess.Concrete;
using Mulkora.DataAccess.Repository;
using Mulkora.Entity.Concrete;

namespace Mulkora.DataAccess.EntityFramework;

public class EfServiceDal : GenericRepository<Service>, IServicesDal
{
    public EfServiceDal(Context context) : base(context)
    {
    }
}