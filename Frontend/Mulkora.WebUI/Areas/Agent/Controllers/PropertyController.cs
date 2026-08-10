using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Mulkora.Dto.PropertyDtos;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Areas.Agent.Controllers;

[Area("Agent")]
[AutoValidateAntiforgeryToken]

public class PropertyController : Controller
{
    private readonly IPropertyService _propertyService;

    public PropertyController(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    // GET
    public async Task<IActionResult> PropertyList()
    {
        var token = User.FindFirstValue("access_token");
        var values = await _propertyService.GetPropertiesByUserIdAsync(token);
        return View(values);
    }

    public IActionResult Create()
    {
        return View();  
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePropertyDto dto)
    {
        var token = User.FindFirstValue("access_token");

        await _propertyService.CreatePropertyAsync(dto, token);

        return RedirectToAction(nameof(PropertyList));
    }

    public async Task<IActionResult> Update(int id)
    {
        var value = await _propertyService.TGetByIdAsync(id);
        return View(value);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdatePropertyDto dto)
    {
        await _propertyService.TUpdateAsync(dto);
        return RedirectToAction(nameof(PropertyList));
    }
    
    [HttpPost]
    public async Task<IActionResult> SendForApproval(int id)
    {
        var token = User.FindFirstValue("access_token");

        await _propertyService.SendForApprovalAsync(id, token!);

        return RedirectToAction(nameof(PropertyList));
    }
}