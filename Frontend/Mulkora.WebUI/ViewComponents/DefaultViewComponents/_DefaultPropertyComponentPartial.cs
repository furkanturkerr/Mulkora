using Microsoft.AspNetCore.Mvc;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.ViewComponents.DefaultViewComponents;

public class _DefaultPropertyComponentPartial : ViewComponent
{
    private readonly IPropertyService _propertyService;

    public _DefaultPropertyComponentPartial(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var value = await _propertyService.GetAllAsync();
        value.Take(5);
        return View(value);
    }
}