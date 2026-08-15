using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mulkora.Dto.FeatureDtos;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
[AutoValidateAntiforgeryToken]

public class FeatureController : Controller
{
    private readonly IFeatureService _featureService;

    public FeatureController(IFeatureService featureService)
    {
        _featureService = featureService;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var values = await _featureService.GetAllAsync();
        return View(values);
    }

    public IActionResult Create()
    {
        return View();   
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateFeatureDto dto)
    {
        var token = User.FindFirstValue("access_token");
        await _featureService.TInsertAsync(dto, token);
        return RedirectToAction(nameof(Index));  
    }

    public async Task<IActionResult> Update(int id)
    {
        var token = User.FindFirstValue("access_token");
        var value = await _featureService.TGetByIdAsync(id, token);
        return View(value);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateFeatureDto dto)
    {
        var token = User.FindFirstValue("access_token");
        await _featureService.TUpdateAsync(dto, token);
        return RedirectToAction(nameof(Index)); 
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var token = User.FindFirstValue("access_token");
        await _featureService.TDeleteAsync(id, token);
        return RedirectToAction(nameof(Index));
    }
}