using AutoMapper;
using Mulkora.Business.Abstract;
using Mulkora.DataAccess.Abstract;
using Mulkora.Dto.AppointmentDtos;
using Mulkora.Entity.Concrete;
using Mulkora.Entity.Enums;

namespace Mulkora.Business.Manager;

public class AppointmentManager : IAppointmentService
{
    private readonly IAppointmentDal _appointmentDal;
    private readonly IMapper _mapper;
    private readonly IPropertyDal _propertyDal;

    public AppointmentManager(IAppointmentDal appointmentDal, IMapper mapper, IPropertyDal propertyDal)
    {
        _appointmentDal = appointmentDal;
        _mapper = mapper;
        _propertyDal = propertyDal;
    }

    public async Task CreateAsync(CreateAppointmentDto dto, string appUserId)
    {
        var property = await _propertyDal.GetByIdAsync(dto.PropertyId);

        if (property == null)
            throw new Exception("İlan bulunamadı.");
        
        if (property.Status != PropertyStatus.Published)
        {
            throw new Exception(
                "Sadece yayındaki ilanlar için randevu oluşturulabilir.");
        }
        
        if (dto.AppointmentDate <= DateTime.UtcNow)
        {
            throw new Exception(
                "Geçmiş bir tarih için randevu oluşturulamaz.");
        }

        var appointment = new Appointment
        {
            AppointmentDate = dto.AppointmentDate,
            PropertyId = property.PropertyId,

            AgentId = property.AgentId,

            AppUserId = appUserId,
            CreatedDate = DateTime.UtcNow
        };
        
        if (appointment.AgentId <= 0)
        {
            throw new Exception(
                "İlana bağlı danışman bilgisi bulunamadı.");
        }
        
        var isCreated = await _appointmentDal.CreateAsync(appointment);
        
        if (!isCreated)
        {
            throw new Exception("Danışmanın seçilen tarih ve saatte başka bir randevusu bulunuyor.");
        }
    }
}