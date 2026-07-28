using Otelvexa.DataAccess.Abstract;
using Otelvexa.DataAccess.Concrete;
using Otelvexa.DataAccess.Repository;
using Otelvexa.Entity.Concrete;

namespace Otelvexa.DataAccess.EntityFramework;

public class EfSubscribeDal : GenericRepository<Subscribe>, ISubscribeDal
{
    public EfSubscribeDal(Context context) : base(context)
    {
    }
}