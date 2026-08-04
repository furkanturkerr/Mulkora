using Microsoft.AspNetCore.Mvc;

namespace Mulkora.WebUI.Controllers;

public class AgentController : Controller
{
    // GET
    [Route("Danismanlar")]
    public IActionResult Index()
    {
        return View();
    }

    [Route("Danismanlar/{id:int}")]
    public IActionResult Details(int id)
    {
        return View();
    }
}