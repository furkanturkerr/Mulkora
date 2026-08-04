using Microsoft.AspNetCore.Mvc;

namespace Mulkora.WebUI.Controllers;

public class AboutController : Controller
{
    // GET
    [Route("Hakkimizda")]
    public IActionResult Index()
    {
        return View();
    }
}