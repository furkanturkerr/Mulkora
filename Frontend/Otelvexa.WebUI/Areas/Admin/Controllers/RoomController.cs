using Microsoft.AspNetCore.Mvc;
using Otelvexa.Dto.RoomDtos;
using Otelvexa.WebUI.Services.Abstract;

namespace Otelvexa.WebUI.Areas.Admin.Controllers;

[Area("Admin")]

public class RoomController : Controller
{
    private readonly IRoomService _roomService;

    public RoomController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    public async Task<IActionResult> Index()
    {
        var values = await _roomService.GetAllAsync();
        return View(values);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRoomDto dto)
    {
        await _roomService.TInsertAsync(dto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var value = await _roomService.TGetByIdAsync(id);
        return View(value);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateRoomDto dto)
    {
        await _roomService.TUpdateAsync(dto);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _roomService.TDeleteAsync(id);
        return RedirectToAction("Index");
    }
}