using Microsoft.AspNetCore.Mvc;

namespace Mulkora.WebUI.ViewComponents.AgentViewComponents;

public class _AgentHeadComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}