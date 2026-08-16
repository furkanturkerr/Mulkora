using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mulkora.Dto.PropertyDtos;
using Mulkora.WebUI.Areas.Agent.Models;
using Mulkora.WebUI.Models;
using Mulkora.WebUI.Services.Abstract;
namespace Mulkora.WebUI.Areas.Agent.Controllers;

[Area("Agent")]
[Authorize(Roles = "Agent")]
[AutoValidateAntiforgeryToken]

public class PropertyController : Controller
{
    private readonly IPropertyService _propertyService;
    private readonly IFeatureService _featureService;
    private readonly IPropertyImageService _propertyImageService;
    private readonly ICategoryService _categoryService;

    public PropertyController(IPropertyService propertyService, IFeatureService featureService, IPropertyImageService propertyImageService, ICategoryService categoryService)
    {
        _propertyService = propertyService;
        _featureService = featureService;
        _propertyImageService = propertyImageService;
        _categoryService = categoryService;
    }

    // GET
    public async Task<IActionResult> PropertyList(string? text, int? IsStatus)
    {
        var token = User.FindFirstValue("access_token");
        var values = await _propertyService.GetPropertiesByUserIdAsync(token, text, IsStatus);
        return View(values);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var features = await _featureService.GetAllAsync();
        
        var categories = await _categoryService.GetAllAsync();

        ViewBag.CategoryList = new SelectList(categories, "CategoryId", "Name");

        var model = new CreatePropertyViewModel
        {
            Property = new CreatePropertyDto(),

            Features = features
                .Where(x => x.IsActive)
                .Select(x => new FeatureOptionViewModel
                {
                    FeatureId = x.FeatureId,
                    Name = x.Name,
                    IsSelected = false
                })
                .ToList()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePropertyViewModel model)
    {
        var token = User.FindFirstValue("access_token");

        var response = await _propertyService.CreatePropertyAsync(model.Property, token!);

        if (!response.IsSuccessStatusCode)
        {
            TempData["ErrorMessage"] = await response.Content.ReadAsStringAsync();

            return RedirectToAction(nameof(Create));
        }

        var propertyId = await response.Content.ReadFromJsonAsync<int>();

        if (model.Images != null && model.Images.Count > 0)
        {
            await _propertyImageService.UploadPropertyImagesAsync(propertyId, model.Images, token!);
        }

        return RedirectToAction(nameof(PropertyList));
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var value = await _propertyService.GetByIdAsync(id);

        if (value == null)
        {
            return NotFound();
        }
        
        var categories = await _categoryService.GetAllAsync();
        
        ViewBag.CategoryList = new SelectList(categories, "CategoryId", "Name");

        
        var token = User.FindFirstValue("access_token");

        var features = await _featureService.GetAllAsync();

        var selectedFeatureIds = value.Features
            .Select(x => x.FeatureId)
            .ToHashSet();

        var propertyImages = await _propertyImageService.GetImagesByPropertyIdAsync(id, token!);

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
                FeatureIds = selectedFeatureIds.ToList()
            },

            Features = features.Select(x => new FeatureOptionViewModel
            {
                FeatureId = x.FeatureId,
                Name = x.Name,
                IsSelected = selectedFeatureIds.Contains(x.FeatureId)
            }).ToList(),
            
            PropertyImages = propertyImages
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

            return RedirectToAction(nameof(Create));
        }

        if (model.NewImages.Count > 0)
        {
            await _propertyImageService.UploadPropertyImagesAsync(model.Property.PropertyId, model.NewImages, token!);
        }
        
        return RedirectToAction(nameof(PropertyList));  
    }
    
    [HttpPost]
    public async Task<IActionResult> SendForApproval(int id)
    {
        var token = User.FindFirstValue("access_token");

        await _propertyService.SendForApprovalAsync(id, token!);

        return RedirectToAction(nameof(PropertyList));
    }

    [HttpPost]
    public async Task<IActionResult> DeleteImage(int imageId, int propertyId)
    {
        var token = User.FindFirstValue("access_token");

        await _propertyImageService.DeleteImageAsync(imageId, token!);

        return RedirectToAction(nameof(Update), new { id = propertyId });
    }
    
    [HttpPost]
    public async Task<IActionResult> Deletes(int id)
    {
        var token = User.FindFirstValue("access_token");
        await _propertyService.TDeleteAsync(id, token);
        return RedirectToAction(nameof(PropertyList));  
    }
    
    [HttpPost]
    public async Task<IActionResult> MarkAsSold(int id)
    {
        var token = User.FindFirstValue("access_token");

        if (string.IsNullOrWhiteSpace(token))
        {
            return Unauthorized();
        }

        var response = await _propertyService.MarkAsSoldAsync(id, token);

        if (!response.IsSuccessStatusCode)
        {
            TempData["ErrorMessage"] =
                await response.Content.ReadAsStringAsync();
        }
        else
        {
            TempData["SuccessMessage"] =
                "İlan satıldı olarak işaretlendi.";
        }

        return RedirectToAction(nameof(PropertyList));
    }

    [HttpPost]
    public async Task<IActionResult> MarkAsRented(int id)
    {
        var token = User.FindFirstValue("access_token");

        if (string.IsNullOrWhiteSpace(token))
        {
            return Unauthorized();
        }

        var response = await _propertyService.MarkAsRentedAsync(id, token);

        if (!response.IsSuccessStatusCode)
        {
            TempData["ErrorMessage"] =
                await response.Content.ReadAsStringAsync();
        }
        else
        {
            TempData["SuccessMessage"] =
                "İlan kiralandı olarak işaretlendi.";
        }

        return RedirectToAction(nameof(PropertyList));
    }
}