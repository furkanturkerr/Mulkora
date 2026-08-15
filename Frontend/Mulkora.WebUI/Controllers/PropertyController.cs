using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mulkora.WebUI.Models;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Controllers;

public class PropertyController : Controller
{
    private readonly IPropertyService _propertyService;
    private readonly ICategoryService _categoryService;
    private readonly IConfiguration _configuration;
    private readonly IPropertyImageService _propertyImageService;

    public PropertyController(IPropertyService propertyService, ICategoryService categoryService, IConfiguration configuration, IPropertyImageService propertyImageService)
    {
        _propertyService = propertyService;
        _categoryService = categoryService;
        _configuration = configuration;
        _propertyImageService = propertyImageService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? city, string? district, int? listingType, int? maxPrice, int? minPrice, int? categoryId, int? roomCount, int page = 1)
    {
        const int pageSize = 9;

        var properties = await _propertyService.GetFilterPropertyAll(city, district, listingType, maxPrice, minPrice, categoryId, roomCount, page, pageSize);
        var categories = await _categoryService.GetAllAsync();

        var firstProperty = properties.FirstOrDefault();

        var model = new PublicPropertyListViewModel
        {
            Properties = properties,

            Categories = categories.Select(x => new SelectListItem
            {
                Value = x.CategoryId.ToString(),
                Text = x.Name,
                Selected = x.CategoryId == categoryId
            }).ToList(),

            City = city,
            District = district,
            ListingType = listingType,
            MaxPrice = maxPrice,
            MinPrice = minPrice,
            CategoryId = categoryId,
            RoomCount = roomCount,
            CurrentPage = firstProperty?.CurrentPage ?? 1,
            TotalPages = firstProperty?.TotalPages ?? 0,
            ApiBaseUrl = (_configuration["ApiSettings:BaseUrl"] ?? "http://localhost:5214").TrimEnd('/')
        };

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var property = await _propertyService.GetPublishedByIdAsync(id);

        if (property == null)
        {
            return NotFound();
        }

        var images = await _propertyImageService.GetPublicImagesByPropertyIdAsync(id);

        var model = new PublicPropertyDetailsViewModel
        {
            Property = property,
            Images = images,
            ApiBaseUrl = (_configuration["ApiSettings:BaseUrl"] ?? "http://localhost:5214").TrimEnd('/')
        };

        return View(model);
    }
}