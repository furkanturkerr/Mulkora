using Microsoft.AspNetCore.Mvc;

namespace Otelvexa.WebUI.ViewComponents.AdminViewComponents;

public class _AdminFooterComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}