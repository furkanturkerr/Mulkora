using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Otelvexa.Entity.Concrete;

namespace Otelvexa.DataAccess.Concrete;

public class Context : IdentityDbContext<AppUser, AppRole, string>
{
    public Context(DbContextOptions<Context> options)
        : base(options)
    {
    }
    
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Staff> Staffs { get; set; }
    public DbSet<Subscribe> Subscribes { get; set; }
    public DbSet<Testimonial> Testimonials { get; set; }
    public DbSet<Service> Services { get; set; }
    
}