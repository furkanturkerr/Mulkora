using Microsoft.AspNetCore.Mvc;
using Mulkora.Dto.AgentDtos;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[AutoValidateAntiforgeryToken]

public class AgentController : Controller
{
    private readonly IAgentService _agentService;

    public AgentController(IAgentService agentService)
    {
        _agentService = agentService;
    }

    // GET
    public async Task<IActionResult> AgentList(string? text, bool? isTrue)
    {
        var values = await _agentService.GetFilterAgent(text, isTrue);
        ViewBag.Text = text;
        ViewBag.IsTrue = isTrue;
        return View(values);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAgentDto dto)
    {
        var response = await _agentService.TInsertAsync(dto);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", error);

            return View(dto);
        }
        return RedirectToAction(nameof(AgentList));
    } 

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var value = await _agentService.TGetByIdAsync(id);
        return View(value);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateAgentDto dto)
    {
        var response = await _agentService.TUpdateAsync(dto);
        
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", error);

            return View(dto);
        }
        
        return RedirectToAction(nameof(AgentList));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _agentService.TDeleteAsync(id);
        return RedirectToAction(nameof(AgentList));
    }
}