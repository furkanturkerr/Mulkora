using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mulkora.Dto.AppointmentDtos;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Controllers;

[Authorize]

public class AppointmentController : Controller
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }
    [HttpGet]
    public IActionResult Index(int propertyId)
    {
        if (propertyId <= 0)
        {
            return BadRequest();
        }

        var dto = new CreateAppointmentDto
        {
            PropertyId = propertyId
        };

        return View(dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(CreateAppointmentDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }
        
        var token = User.FindFirstValue("access_token");

        if (string.IsNullOrWhiteSpace(token))
        {
            return Unauthorized();
        }

        var response = await _appointmentService.CreateAsync(dto, token);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();

            ModelState.AddModelError("", error);

            return View(dto);
        }

        var turkishCulture =
            CultureInfo.GetCultureInfo("tr-TR");

        TempData["AppointmentDate"] =
            dto.AppointmentDate.ToString(
                "dd MMMM yyyy, HH:mm",
                turkishCulture);

        return RedirectToAction(
            nameof(Success),
            new { propertyId = dto.PropertyId });
    }

    [HttpGet]
    public IActionResult Success(int propertyId)
    {
        if (propertyId <= 0)
        {
            return RedirectToAction(
                "Index",
                "Property");
        }

        ViewBag.PropertyId = propertyId;

        return View();
    }
}