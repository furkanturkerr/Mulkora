using Microsoft.AspNetCore.Mvc;
using Mulkora.Dto.RoleDtos;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[AutoValidateAntiforgeryToken]

public class RoleController : Controller
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    // GET
    public async Task<IActionResult> RoleList()
    {
        var values = await _roleService.GetAllAsync();
        return View(values);
    }
    
    public IActionResult Create()
    {
        return View();   
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRoleDto dto)
    {
        await _roleService.TInsertAsync(dto);
        return RedirectToAction(nameof(RoleList)); 
    }

    public async Task<IActionResult> Update(string id)
    {
        var value = await _roleService.GetRoleByIdAsync(id);
        return View(value);   
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateRoleDto dto)
    {
        await _roleService.TUpdateAsync(dto);
        return RedirectToAction(nameof(RoleList));
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        await _roleService.DeleteRoleAsync(id);
        return RedirectToAction(nameof(RoleList));
    }
}