using Mulkora.Dto.AppointmentDtos;

namespace Mulkora.WebUI.Services.Abstract;

public interface IAppointmentService
{
    Task<HttpResponseMessage> CreateAsync(CreateAppointmentDto dto, string token);
}