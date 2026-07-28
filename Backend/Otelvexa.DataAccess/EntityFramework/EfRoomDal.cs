using Otelvexa.DataAccess.Abstract;
using Otelvexa.DataAccess.Concrete;
using Otelvexa.DataAccess.Repository;
using Otelvexa.Entity.Concrete;

namespace Otelvexa.DataAccess.EntityFramework;

public class EfRoomDal : GenericRepository<Room>,  IRoomDal
{
    public EfRoomDal(Context context) : base(context)
    {
    }
}