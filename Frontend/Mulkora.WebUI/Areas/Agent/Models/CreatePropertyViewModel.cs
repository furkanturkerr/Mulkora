using Mulkora.Dto.PropertyDtos;
using Mulkora.WebUI.Models;

namespace Mulkora.WebUI.Areas.Agent.Models;

public class CreatePropertyViewModel
{
    public CreatePropertyDto Property { get; set; } = new();

    public List<FeatureOptionViewModel> Features { get; set; } = new();
    
    public List<IFormFile> Images { get; set; } = new List<IFormFile>();
}