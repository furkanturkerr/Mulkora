using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mulkora.Entity.Concrete;

namespace Mulkora.DataAccess.Concrete;

public class Context : IdentityDbContext<AppUser, AppRole, string>
{
    public Context(DbContextOptions<Context> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<Agent>()
            .HasOne(x => x.AppUser)
            .WithOne(x => x.Agent)
            .HasForeignKey<Agent>(x => x.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public DbSet<Contact> Contacts { get; set; }
    
    public DbSet<Agent> Agents { get; set; }
    
    public DbSet<Property> Properties { get; set; }

    public DbSet<PropertyImage> PropertyImages { get; set; }

    public DbSet<Category> Categories { get; set; }
    
    public DbSet<Feature> Features { get; set; }
}