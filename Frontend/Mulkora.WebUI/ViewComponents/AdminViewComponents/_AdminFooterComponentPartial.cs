using Microsoft.AspNetCore.Mvc;

namespace Mulkora.WebUI.ViewComponents.AdminViewComponents;

public class _AdminFooterComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}