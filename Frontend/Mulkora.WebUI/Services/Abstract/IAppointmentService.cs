using Mulkora.Dto.AppointmentDtos;

namespace Mulkora.WebUI.Services.Abstract;

public interface IAppointmentService
{
    Task<HttpResponseMessage> CreateAsync(CreateAppointmentDto dto, string token);
    Task<List<ResultAppointmentDto>> GetAppointmentsByUserIdAsync(string userId, string token);
    Task<List<ResultAppointmentDto>> GetAppointmentsByAgentUserIdAsync(string token);
    Task ApproveAsync(int id, string token);
}