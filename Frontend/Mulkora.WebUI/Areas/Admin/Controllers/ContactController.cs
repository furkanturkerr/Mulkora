using Microsoft.AspNetCore.Mvc;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Areas.Admin.Controllers;

public class ContactController : Controller
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var values = await _contactService.GetAllAsync();
        return View(values);
    }
}