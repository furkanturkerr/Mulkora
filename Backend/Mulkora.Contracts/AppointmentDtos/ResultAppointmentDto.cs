namespace Mulkora.Dto.AppointmentDtos;

public class ResultAppointmentDto
{
    public int AppointmentId { get; set; }

    public DateTime AppointmentDate { get; set; }
    
    public int PropertyId { get; set; }
    
    public string PropertyAddress { get; set; } = string.Empty;
    
    public string Title { get; set; } = string.Empty;

    public int AgentId { get; set; }
    
    public string AgentNameSurname{ get; set; } = string.Empty;
    
    public bool IsStatus { get; set; }
}