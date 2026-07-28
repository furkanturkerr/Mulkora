using Microsoft.AspNetCore.Mvc;

namespace Otelvexa.WebUI.ViewComponents.AdminViewComponents;

public class _AdminSidebarComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}