using Microsoft.AspNetCore.Mvc;

namespace Mulkora.WebUI.ViewComponents.DefaultViewComponents;

public class _DefaultHeaderComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke() => View();
}