using Microsoft.AspNetCore.Mvc;
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
        var values = await _propertyService.GetAllAsync();
        return View(values);
    }

    public IActionResult Create()
    {
        return View();  
    }
}