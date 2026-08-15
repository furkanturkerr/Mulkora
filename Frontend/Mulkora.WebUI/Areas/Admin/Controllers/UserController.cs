using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mulkora.Dto.UserDtos;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
[AutoValidateAntiforgeryToken]

public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // GET
    public async Task<IActionResult> UserList()
    {
        var token = User.FindFirstValue("access_token");
        var values = await _userService.GetUsers(token);
        return View(values);
    }
    
    [HttpGet]
    public async Task<IActionResult> RoleAssign(string id)
    {
        var token = User.FindFirstValue("access_token");
        
        var value = await _userService.GetUserRolesAsync(id, token);

        return View(value);
    }

    [HttpPost]
    public async Task<IActionResult> RoleAssign(RoleAssignDto dto)
    {
        var token = User.FindFirstValue("access_token");
        
        await _userService.UpdateUserRolesAsync(dto, token);

        return RedirectToAction("UserList");
    }
}