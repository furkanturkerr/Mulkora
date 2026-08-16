using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Areas.Agent.Controllers;

[Area("Agent")]
[Authorize(Roles = "Agent")]
[AutoValidateAntiforgeryToken]

public class AppointmentController : Controller
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    // GET
    public async Task<IActionResult> AppointmentList()
    {
        var token = User.FindFirstValue("access_token");
        var values = await _appointmentService.GetAppointmentsByAgentUserIdAsync(token);
        return View(values);
    }

    [HttpPost]
    public async Task<IActionResult> Approve(int id)
    {
        var token = User.FindFirstValue("access_token");
        await _appointmentService.ApproveAsync(id, token);
        return RedirectToAction(nameof(AppointmentList));
    }
}