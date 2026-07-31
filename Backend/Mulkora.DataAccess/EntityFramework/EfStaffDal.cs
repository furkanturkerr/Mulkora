using Mulkora.DataAccess.Abstract;
using Mulkora.DataAccess.Concrete;
using Mulkora.DataAccess.Repository;
using Mulkora.Entity.Concrete;

namespace Mulkora.DataAccess.EntityFramework;

public class EfStaffDal : GenericRepository<Staff>, IStaffDal
{
    public EfStaffDal(Context context) : base(context)
    {
    }
}