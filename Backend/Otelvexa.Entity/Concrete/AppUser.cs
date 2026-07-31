using Microsoft.AspNetCore.Identity;

namespace Otelvexa.Entity.Concrete;

public class AppUser : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? City { get; set; }
    public string? ImageUrl { get; set; }
    public string? Country { get; set; }
    public string? Gender { get; set; }
}