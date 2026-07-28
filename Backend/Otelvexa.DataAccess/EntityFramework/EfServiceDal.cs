using Otelvexa.DataAccess.Abstract;
using Otelvexa.DataAccess.Concrete;
using Otelvexa.DataAccess.Repository;
using Otelvexa.Entity.Concrete;

namespace Otelvexa.DataAccess.EntityFramework;

public class EfServiceDal : GenericRepository<Service>, IServicesDal
{
    public EfServiceDal(Context context) : base(context)
    {
    }
}