using Microsoft.AspNetCore.Mvc;

namespace Mulkora.WebUI.ViewComponents.AgentViewComponents;

public class _AgentHeaderComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke() => View();
}