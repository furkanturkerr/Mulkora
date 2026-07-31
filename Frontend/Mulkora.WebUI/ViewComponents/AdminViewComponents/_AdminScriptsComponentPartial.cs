using Microsoft.AspNetCore.Mvc;

namespace Mulkora.WebUI.ViewComponents.AdminViewComponents;

public class _AdminScriptsComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}