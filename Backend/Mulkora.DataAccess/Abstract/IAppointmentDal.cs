using Mulkora.Entity.Concrete;

namespace Mulkora.DataAccess.Abstract;

public interface IAppointmentDal
{
    Task<bool> CreateAsync(Appointment appointment);
    Task<List<Appointment>> GetAppointmentsByUserIdAsync(string userId);
    Task<List<Appointment>> GetAppointmentsByAgentUserIdAsync(string userId);
    Task ApproveAsync(int id, int agentId);
}