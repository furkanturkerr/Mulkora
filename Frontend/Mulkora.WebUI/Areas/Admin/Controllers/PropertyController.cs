using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mulkora.Dto.PropertyDtos;
using Mulkora.WebUI.Areas.Admin.Models;
using Mulkora.WebUI.Models;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
[AutoValidateAntiforgeryToken]

public class PropertyController : Controller
{
    private readonly IPropertyService _propertyService;
    private readonly IAgentService _agentService;
    private readonly IFeatureService _featureService;
    private readonly ICategoryService _categoryService;
    private readonly IPropertyImageService _propertyImageService;
    public PropertyController(IPropertyService propertyService, IAgentService agentService, ICategoryService categoryService, IFeatureService featureService, IPropertyImageService propertyImageService)
    {
        _propertyService = propertyService;
        _agentService = agentService;
        _categoryService = categoryService;
        _featureService = featureService;
        _propertyImageService = propertyImageService;
    }

    // GET
    public async Task<IActionResult> PropertyList(string? text, int? IsStatus, string? City, string? District, int? ListingType, int page = 1)
    {
        var token = User.FindFirstValue("access_token");
        const int pageSize = 8;
        var values = await _propertyService.GetFilterProperty(text, IsStatus, City, District, ListingType, page, pageSize, token);
        return View(values);
    }
    
    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var value = await _propertyService.GetByIdAsync(id);

        var categories = await _categoryService.GetAllAsync();

        ViewBag.CategoryList = new SelectList(
            categories,
            "CategoryId",
            "Name",
            value.CategoryId
        );

        var features = await _featureService.GetAllAsync();

        var selectedFeatureIds = value.Features
            .Select(x => x.FeatureId)
            .ToList();

        var model = new UpdatePropertyViewModel
        {
            Property = new UpdatePropertyDto
            {
                PropertyId = value.PropertyId,
                Title = value.Title,
                Description = value.Description,
                Price = value.Price,
                ListingType = value.ListingType,
                CategoryId = value.CategoryId,
                City = value.City,
                District = value.District,
                Address = value.Address,
                RoomCount = value.RoomCount,
                LivingRoomCount = value.LivingRoomCount,
                BathroomCount = value.BathroomCount,
                GrossSquareMeter = value.GrossSquareMeter,
                NetSquareMeter = value.NetSquareMeter,
                BuildingAge = value.BuildingAge,
                FloorNumber = value.FloorNumber,
                TotalFloor = value.TotalFloor,
                IsFurnished = value.IsFurnished,
                IsFeatured = value.IsFeatured,
                FeatureIds = selectedFeatureIds
            },

            Features = features.Select(x => new FeatureOptionViewModel
            {
                FeatureId = x.FeatureId,
                Name = x.Name,
                IsSelected = selectedFeatureIds.Contains(x.FeatureId)
            }).ToList()
        };

        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> Update(UpdatePropertyViewModel model)
    {
        var token = User.FindFirstValue("access_token");
        
        var response = await _propertyService.UpdatePropertyAsync(model.Property, token!);
        
        if (!response.IsSuccessStatusCode)
        {
            TempData["ErrorMessage"] = await response.Content.ReadAsStringAsync();

            return RedirectToAction(nameof(Update), new { id = model.Property.PropertyId});
        }

        return RedirectToAction(nameof(PropertyList));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var token = User.FindFirstValue("access_token");
        await _propertyService.TDeleteAsync(id, token);
        return RedirectToAction(nameof(PropertyList));  
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var token = User.FindFirstValue("access_token");

        if (string.IsNullOrWhiteSpace(token))
        {
            return Unauthorized();
        }

        var property = await _propertyService.GetByIdAsync(id);

        if (property == null)
        {
            return NotFound();
        }

        var propertyImages = await _propertyImageService.GetImagesByPropertyIdAsync(id, token);

        var model = new PropertyDetailsViewModel
        {
            Property = property,
            PropertyImages = propertyImages
        };

        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> Approve(int id)
    {
        var token = User.FindFirstValue("access_token");

        await _propertyService.ApproveAsync(id, token!);
        
        return RedirectToAction(nameof(PropertyList));
    }

    [HttpPost]
    public async Task<IActionResult> Reject(int id)
    {
        var token = User.FindFirstValue("access_token");

        await _propertyService.RejectAsync(id, token!);
        
        return RedirectToAction(nameof(PropertyList));
    }
}