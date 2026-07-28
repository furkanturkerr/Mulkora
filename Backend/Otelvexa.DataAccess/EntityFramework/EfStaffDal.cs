using Otelvexa.DataAccess.Abstract;
using Otelvexa.DataAccess.Concrete;
using Otelvexa.DataAccess.Repository;
using Otelvexa.Entity.Concrete;

namespace Otelvexa.DataAccess.EntityFramework;

public class EfStaffDal : GenericRepository<Staff>, IStaffDal
{
    public EfStaffDal(Context context) : base(context)
    {
    }
}