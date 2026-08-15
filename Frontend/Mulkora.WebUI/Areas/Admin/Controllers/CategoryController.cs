using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mulkora.Dto.CategoryDtos;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
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
        var token = User.FindFirstValue("access_token");
        await _categoryService.TInsertAsync(dto, token);
        return RedirectToAction(nameof(Index));   
    }
    
    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var token = User.FindFirstValue("access_token");
        var value = await _categoryService.TGetByIdAsync(id, token);
        return View(value);
    }
    
    [HttpPost]
    public async Task<IActionResult> Update(UpdateCategoryDto dto)
    {
        var token = User.FindFirstValue("access_token");
        await _categoryService.TUpdateAsync(dto, token);
        return RedirectToAction(nameof(Index));   
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var token = User.FindFirstValue("access_token");
        await _categoryService.TDeleteAsync(id, token);
        return RedirectToAction(nameof(Index));   
    }
}