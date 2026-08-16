using Mulkora.Dto.AppointmentDtos;

namespace Mulkora.Business.Abstract;

public interface IAppointmentService
{
    Task CreateAsync(CreateAppointmentDto dto, string appUserId);
    Task<List<ResultAppointmentDto>> TGetAppointmentsByUserIdAsync(string userId);
    Task<List<ResultAppointmentDto>> TGetAppointmentsByAgentUserIdAsync(string userId);
    Task ApproveAsync(int id, int agentId);
}