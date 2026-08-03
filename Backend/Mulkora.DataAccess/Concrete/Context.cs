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
    
    DbSet<Contact> Contacts { get; set; }
}