using Microsoft.AspNetCore.Mvc;
using Mulkora.Dto.ContactDtos;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Controllers;

public class ContactController : Controller
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    // GET
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateMessage(CreateContactDto dto)
    {
        if (!ModelState.IsValid)
            return View("Index", dto);
        
        dto.MessageDate = DateTime.Now;
        await _contactService.TInsertAsync(dto);
        TempData["SuccessMessage"] = "Mesajınız başarıyla gönderildi.";
        return RedirectToAction("Index", "Contact");
    }
}