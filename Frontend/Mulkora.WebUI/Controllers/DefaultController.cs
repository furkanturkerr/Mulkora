using Microsoft.AspNetCore.Mvc;

namespace Mulkora.WebUI.Controllers;

public class DefaultController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}