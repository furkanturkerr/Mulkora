using System.Data;
using Microsoft.EntityFrameworkCore;
using Mulkora.DataAccess.Abstract;
using Mulkora.DataAccess.Concrete;
using Mulkora.Entity.Concrete;

namespace Mulkora.DataAccess.EntityFramework;

public class EfAppointmentDal : IAppointmentDal
{
    private readonly Context _context;

    public EfAppointmentDal(Context context)
    {
        _context = context;
    }
    
    // Transaction başlatılır.
    // Danışmanın seçilen saatte başka randevusu var mı kontrol edilir.
    // Varsa işlem geri alınır ve false döner.
    // Yoksa randevu eklenir.
    // SaveChangesAsync() başarılı olursa transaction onaylanır.
    // Beklenmeyen hata çıkarsa catch çalışır, transaction geri alınır.

    public async Task<bool> CreateAsync(Appointment appointment)
    {
        await using var transaction =
            await _context.Database.BeginTransactionAsync(
                IsolationLevel.Serializable);
        
        //IsolationLevel.Serializable kullanmamızın nedeni,
        //iki kullanıcının aynı anda aynı saati seçmesi durumunda ikisinin de kontrolü geçmesini engellemektir.
        
        try
        {
            var isAppointmentExists = await _context.Appointments.AnyAsync(x =>
                x.AgentId == appointment.AgentId &&
                x.AppointmentDate == appointment.AppointmentDate);
            
            if (isAppointmentExists)
            {
                await transaction.RollbackAsync();
                return false;
            }
            
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<List<Appointment>> GetAppointmentsByUserIdAsync(
        string userId)
    {
        return await _context.Appointments
            .Where(x => x.AppUserId == userId)
            .Include(x => x.Property)
            .Include(x => x.Agent)
            .ThenInclude(x => x.AppUser)
            .OrderByDescending(x => x.AppointmentDate)
            .AsNoTracking()
            .ToListAsync();
    }
    
    public async Task<List<Appointment>> GetAppointmentsByAgentUserIdAsync(
        string userId)
    {
        return await _context.Appointments
            .Where(x => x.Agent.AppUserId == userId)
            .Include(x => x.Property)
            .Include(x => x.AppUser)
            .OrderByDescending(x => x.AppointmentDate)
            .AsNoTracking()
            .ToListAsync();
    }
}