using Mulkora.Entity.Concrete;

namespace Mulkora.DataAccess.Abstract;

public interface IAppointmentDal
{
    Task<bool> CreateAsync(Appointment appointment);
}