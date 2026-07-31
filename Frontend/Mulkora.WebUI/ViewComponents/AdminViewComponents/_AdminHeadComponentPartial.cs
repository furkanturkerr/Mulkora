using Microsoft.AspNetCore.Mvc;

namespace Mulkora.WebUI.ViewComponents.AdminViewComponents;

public class _AdminHeadComponentPartial : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}