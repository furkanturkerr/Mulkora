using Microsoft.AspNetCore.Mvc;

namespace Mulkora.WebUI.Controllers;

public class ErrorController : Controller
{
    [Route("Error/401")]
    public IActionResult UnauthorizedPage()
    {
        Response.StatusCode = 401;
        return View("Unauthorized");
    }

    [Route("Error/403")]
    public IActionResult Forbidden()
    {
        Response.StatusCode = 403;
        return View();
    }

    [Route("Error/404")]
    public IActionResult NotFoundPage()
    {
        Response.StatusCode = 404;
        return View("NotFound");
    }
    
    [Route("Error/500")]
    public IActionResult InternalServerError()
    {
        return View();
    }
}