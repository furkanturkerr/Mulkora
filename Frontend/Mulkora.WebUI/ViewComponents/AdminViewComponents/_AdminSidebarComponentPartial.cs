using Microsoft.AspNetCore.Mvc;

namespace Mulkora.WebUI.ViewComponents.AdminViewComponents;

public class _AdminSidebarComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}