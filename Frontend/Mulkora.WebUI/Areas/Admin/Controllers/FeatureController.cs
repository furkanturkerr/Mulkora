using Microsoft.AspNetCore.Mvc;
using Mulkora.Dto.FeatureDtos;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
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
        await _featureService.TInsertAsync(dto);
        return RedirectToAction(nameof(Index));  
    }

    public async Task<IActionResult> Update(int id)
    {
        var value = await _featureService.TGetByIdAsync(id);
        return View(value);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateFeatureDto dto)
    {
        await _featureService.TUpdateAsync(dto);
        return RedirectToAction(nameof(Index)); 
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _featureService.TDeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}