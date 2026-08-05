using Microsoft.AspNetCore.Mvc;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
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
        if (page < 1)
            page = 1;
        
        const int pageSize = 5;
        
        var values = await _contactService.GetFullListAsync(page);
        
        ViewBag.CurrentPage = page;
        ViewBag.HasNextPage = values.Count == pageSize;

        return View(values);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _contactService.TDeleteAsync(id);
        return RedirectToAction(nameof(MessageList));
    }
}