using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mulkora.Dto.AgentDtos;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
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
        var token = User.FindFirstValue("access_token");
        
        var values = await _agentService.GetFilterAgent(text, isTrue, token!);
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
        var token = User.FindFirstValue("access_token");

        if (token == null)
            return Unauthorized();
        
        var response = await _agentService.TInsertAsync(dto, token);

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
        var token = User.FindFirstValue("access_token");
        var value = await _agentService.TGetByIdAsync(id, token);
        return View(value);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateAgentDto dto)
    {
        var token = User.FindFirstValue("access_token");
        var response = await _agentService.TUpdateAsync(dto, token);
        
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
        var token = User.FindFirstValue("access_token");
        await _agentService.TDeleteAsync(id, token);
        return RedirectToAction(nameof(AgentList));
    }
}