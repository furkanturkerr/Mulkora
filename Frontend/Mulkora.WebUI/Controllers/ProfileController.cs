using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Controllers;

public class ProfileController : Controller
{
    private readonly IAppointmentService _appointmentService;

    public ProfileController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    // GET
    public async Task<IActionResult> Appointments()
    {
        var token = User.FindFirstValue("access_token");
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var value = await _appointmentService.GetAppointmentsByUserIdAsync(userId, token);
        
        return View(value);
    }
}