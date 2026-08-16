using Microsoft.AspNetCore.Mvc;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.ViewComponents.DefaultViewComponents;

public class _DefaultPropertyComponentPartial : ViewComponent
{
    private readonly IPropertyService _propertyService;

    public _DefaultPropertyComponentPartial(
        IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var values = await _propertyService.GetFilterPropertyAll(
            city: null,
            district: null,
            listingType: null,
            maxPrice: null,
            minPrice: null,
            categoryId: null,
            roomCount: null,
            page: 1,
            pageSize: 3
        );

        return View(values);
    }
}