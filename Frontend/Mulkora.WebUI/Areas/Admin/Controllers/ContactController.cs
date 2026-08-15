using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
[AutoValidateAntiforgeryToken]

public class ContactController : Controller
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    // GET
    public async Task<IActionResult> MessageList(int page = 1)
    {
        var token = User.FindFirstValue("access_token");
        if (page < 1)
            page = 1;
        
        const int pageSize = 5;
        
        var values = await _contactService.GetFullListAsync(page, token);
        
        ViewBag.CurrentPage = page;
        ViewBag.HasNextPage = values.Count == pageSize;

        return View(values);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var token = User.FindFirstValue("access_token");
        await _contactService.TDeleteAsync(id, token);
        return RedirectToAction(nameof(MessageList));
    }
}