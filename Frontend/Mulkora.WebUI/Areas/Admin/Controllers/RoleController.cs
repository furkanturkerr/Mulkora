using System.Security.Claims;
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
        var token = User.FindFirstValue("access_token");
        var values = await _roleService.GetRoles(token);
        return View(values);
    }
    
    public IActionResult Create()
    {
        return View();   
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRoleDto dto)
    {
        var token = User.FindFirstValue("access_token");
        await _roleService.TInsertAsync(dto, token);
        return RedirectToAction(nameof(RoleList)); 
    }

    public async Task<IActionResult> Update(string id)
    {
        var token = User.FindFirstValue("access_token");
        var value = await _roleService.GetRoleByIdAsync(id, token);
        return View(value);   
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateRoleDto dto)
    {
        var token = User.FindFirstValue("access_token");
        await _roleService.TUpdateAsync(dto, token);
        return RedirectToAction(nameof(RoleList));
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var token = User.FindFirstValue("access_token");
        await _roleService.DeleteRoleAsync(id, token);
        return RedirectToAction(nameof(RoleList));
    }
}