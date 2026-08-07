using Microsoft.AspNetCore.Mvc;
using Mulkora.Dto.CategoryDtos;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[AutoValidateAntiforgeryToken]

public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var values = await _categoryService.GetAllAsync();
        return View(values);
    }
    
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryDto dto)
    {
        await _categoryService.TInsertAsync(dto);
        return RedirectToAction(nameof(Index));   
    }
    
    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var value = await _categoryService.TGetByIdAsync(id);
        return View(value);
    }
    
    [HttpPost]
    public async Task<IActionResult> Update(UpdateCategoryDto dto)
    {
        await _categoryService.TUpdateAsync(dto);
        return RedirectToAction(nameof(Index));   
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _categoryService.TDeleteAsync(id);
        return RedirectToAction(nameof(Index));   
    }
}