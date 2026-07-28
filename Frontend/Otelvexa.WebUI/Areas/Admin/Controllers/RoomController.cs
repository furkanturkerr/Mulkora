using Microsoft.AspNetCore.Mvc;

namespace Otelvexa.WebUI.Areas.Admin.Controllers;

public class RoomController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}