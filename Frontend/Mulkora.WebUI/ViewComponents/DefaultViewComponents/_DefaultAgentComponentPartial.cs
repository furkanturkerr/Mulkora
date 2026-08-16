using Microsoft.AspNetCore.Mvc;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.ViewComponents.DefaultViewComponents;

public class _DefaultAgentComponentPartial : ViewComponent
{
    private readonly IAgentService _agentService;

    public _DefaultAgentComponentPartial(
        IAgentService agentService)
    {
        _agentService = agentService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var agents = await _agentService.GetListAgentTrue();

        return View(agents);
    }
}