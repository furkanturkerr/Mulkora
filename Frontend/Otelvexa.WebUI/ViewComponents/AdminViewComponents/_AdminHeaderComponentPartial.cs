using Microsoft.AspNetCore.Mvc;

namespace Otelvexa.WebUI.ViewComponents.AdminViewComponents;

public class _AdminHeaderComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}