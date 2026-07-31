using Microsoft.AspNetCore.Mvc;
using Mulkora.Dto.StaffDtos;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Areas.Admin.Controllers;

[Area("Admin")]

public class StaffController : Controller
{
    private readonly IStaffService _service;

    public StaffController(IStaffService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var values = await _service.GetAllAsync();
        return View(values);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateStaffDto dto)
    {
        await _service.TInsertAsync(dto);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var value = await _service.TGetByIdAsync(id);
        return View(value);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateStaffDto dto)
    {
        await _service.TUpdateAsync(dto);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _service.TDeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}