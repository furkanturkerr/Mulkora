using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mulkora.Dto.PropertyDtos;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[AutoValidateAntiforgeryToken]

public class PropertyController : Controller
{
    private readonly IPropertyService _propertyService;
    private readonly IAgentService _agentService;
    private readonly ICategoryService _categoryService;

    public PropertyController(IPropertyService propertyService, IAgentService agentService, ICategoryService categoryService)
    {
        _propertyService = propertyService;
        _agentService = agentService;
        _categoryService = categoryService;
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
        var category = await _categoryService.GetAllAsync();
        SelectList categoryList = new SelectList(category, "CategoryId", "Name");
        ViewBag.CategoryList = categoryList;
        
        var value = await _propertyService.TGetByIdAsync(id);
        return View(value);
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
}