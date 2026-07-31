using Microsoft.AspNetCore.Mvc;

namespace Mulkora.WebUI.ViewComponents.AdminViewComponents;

public class _AdminHeaderComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}