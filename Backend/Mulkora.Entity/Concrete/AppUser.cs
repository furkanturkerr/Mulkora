using Microsoft.AspNetCore.Identity;

namespace Mulkora.Entity.Concrete;

public class AppUser : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
    
    public Agent? Agent { get; set; }
    public List<Appointment> Appointments { get; set; } = new List<Appointment>();}