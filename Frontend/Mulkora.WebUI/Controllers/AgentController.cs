using Microsoft.AspNetCore.Mvc;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Controllers;

public class AgentController : Controller
{
    private readonly IAgentService _agentService;

    public AgentController(IAgentService agentService)
    {
        _agentService = agentService;
    }

    // GET
    [Route("Danismanlar")]
    public async Task<IActionResult> Index()
    {
        var value = await _agentService.GetListAgentTrue();
        return View(value);
    }

    [Route("Danismanlar/{id:int}")]
    public async Task<IActionResult> Details(int id)
    {
        var value = await _agentService.GetAgentByIdAsync(id);
        return View(value);
    }
}