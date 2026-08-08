using Microsoft.AspNetCore.Mvc;

namespace Mulkora.WebUI.ViewComponents.AgentViewComponents;

public class _AgentSidebarComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke() => View();
}