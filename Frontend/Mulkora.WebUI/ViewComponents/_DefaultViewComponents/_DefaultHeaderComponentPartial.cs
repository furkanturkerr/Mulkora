using Microsoft.AspNetCore.Mvc;

namespace Mulkora.WebUI.ViewComponents._DefaultViewComponents;

public class _DefaultHeaderComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke() => View();
}