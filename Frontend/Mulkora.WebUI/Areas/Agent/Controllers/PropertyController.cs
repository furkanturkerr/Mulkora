using Microsoft.AspNetCore.Mvc;

namespace Mulkora.WebUI.Areas.Agent.Controllers;

[Area("Agent")]

public class PropertyController : Controller
{
    // GET
    public IActionResult PropertyList()
    {
        return View();
    }

    public IActionResult Create()
    {
        return View();  
    }
}