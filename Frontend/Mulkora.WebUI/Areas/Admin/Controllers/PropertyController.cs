using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mulkora.Dto.PropertyDtos;
using Mulkora.WebUI.Areas.Admin.Models;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[AutoValidateAntiforgeryToken]

public class PropertyController : Controller
{
    private readonly IPropertyService _propertyService;
    private readonly IAgentService _agentService;
    private readonly IFeatureService _featureService;
    private readonly ICategoryService _categoryService;

    public PropertyController(IPropertyService propertyService, IAgentService agentService, ICategoryService categoryService, IFeatureService featureService)
    {
        _propertyService = propertyService;
        _agentService = agentService;
        _categoryService = categoryService;
        _featureService = featureService;
    }

    // GET
    public async Task<IActionResult> PropertyList()
    {
        var values = await _propertyService.GetAllAsync();
        return View(values);
    }
    
    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var value = await _propertyService.TGetByIdAsync(id);

        var categories = await _categoryService.GetAllAsync();

        ViewBag.CategoryList = new SelectList(
            categories,
            "CategoryId",
            "Name",
            value.CategoryId
        );

        var features = await _featureService.GetAllAsync();

        var model = new UpdatePropertyViewModel
        {
            Property = value,

            Features = features.Select(x => new FeatureOptionViewModel
            {
                FeatureId = x.FeatureId,
                Name = x.Name,
                IsSelected = value.FeatureIds.Contains(x.FeatureId)
            }).ToList()
        };

        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> Update(UpdatePropertyDto dto)
    {
        await _propertyService.TUpdateAsync(dto);
        return RedirectToAction(nameof(PropertyList));   
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _propertyService.TDeleteAsync(id);
        return RedirectToAction(nameof(PropertyList));  
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var value = await _propertyService.GetByIdAsync(id);
        return View(value);
    }
    
    [HttpPost]
    public async Task<IActionResult> Approve(int id)
    {
        var token = User.FindFirstValue("access_token");

        await _propertyService.ApproveAsync(id, token!);
        
        return RedirectToAction(nameof(PropertyList));
    }

    [HttpPost]
    public async Task<IActionResult> Reject(int id)
    {
        var token = User.FindFirstValue("access_token");

        await _propertyService.RejectAsync(id, token!);
        
        return RedirectToAction(nameof(PropertyList));
    }
}