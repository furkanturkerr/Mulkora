using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Otelvexa.Dto.StaffDtos;

namespace Otelvexa.WebUI.Areas.Admin.Controllers;

[Area("Admin")]

public class StaffController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public StaffController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync("http://localhost:5214/api/Staffs");
        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultStaffDto>>(jsonData);
            return View(values);
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateStaffDto dto)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.PostAsJsonAsync("http://localhost:5214/api/Staffs", dto);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        return View(dto);
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync($"http://localhost:5214/api/Staffs/{id}");
        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<UpdateStaffDto>(jsonData);
            return  View(values);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateStaffDto dto)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.PutAsJsonAsync("http://localhost:5214/api/Staffs", dto);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        return View(dto);
    }
    
    public async Task<IActionResult> Delete(int id)
    {
        var client = _httpClientFactory.CreateClient();
        await client.DeleteAsync($"http://localhost:5214/api/Staffs/{id}");
        return RedirectToAction("Index");
    }
}