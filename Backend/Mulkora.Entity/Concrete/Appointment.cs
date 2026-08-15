namespace Mulkora.Entity.Concrete;

public class Appointment
{
    public int AppointmentId { get; set; }

    public DateTime AppointmentDate { get; set; }

    public string AppUserId { get; set; } = string.Empty;

    public int PropertyId { get; set; }

    public int AgentId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public AppUser AppUser { get; set; } = null!;

    public Property Property { get; set; } = null!;

    public Agent Agent { get; set; } = null!;
}