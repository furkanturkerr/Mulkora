using Mulkora.Dto.AppointmentDtos;

namespace Mulkora.Business.Abstract;

public interface IAppointmentService
{
    Task CreateAsync(CreateAppointmentDto dto, string appUserId);
}