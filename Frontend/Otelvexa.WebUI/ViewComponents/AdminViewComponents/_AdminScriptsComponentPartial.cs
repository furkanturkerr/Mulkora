using Microsoft.AspNetCore.Mvc;

namespace Otelvexa.WebUI.ViewComponents.AdminViewComponents;

public class _AdminScriptsComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}