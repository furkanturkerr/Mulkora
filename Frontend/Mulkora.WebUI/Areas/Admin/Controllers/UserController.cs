using Microsoft.AspNetCore.Mvc;
using Mulkora.Dto.UserDtos;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
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
        var values = await _userService.GetAllAsync();
        return View(values);
    }
    
    [HttpGet]
    public async Task<IActionResult> RoleAssign(string id)
    {
        var value = await _userService.GetUserRolesAsync(id);

        return View(value);
    }

    [HttpPost]
    public async Task<IActionResult> RoleAssign(RoleAssignDto dto)
    {
        await _userService.UpdateUserRolesAsync(dto);

        return RedirectToAction("UserList");
    }
}