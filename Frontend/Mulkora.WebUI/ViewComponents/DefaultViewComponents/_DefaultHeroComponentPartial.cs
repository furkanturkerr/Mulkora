using Microsoft.AspNetCore.Mvc;

namespace Mulkora.WebUI.ViewComponents.DefaultViewComponents;

public class _DefaultHeroComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke() => View();
}