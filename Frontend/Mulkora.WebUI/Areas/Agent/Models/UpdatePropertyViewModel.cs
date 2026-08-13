using Mulkora.Dto.PropertyDtos;
using Mulkora.Dto.PropertyImageDtos;
using Mulkora.WebUI.Models;

namespace Mulkora.WebUI.Areas.Agent.Models;

public class UpdatePropertyViewModel
{
    public UpdatePropertyDto Property { get; set; } = new();

    public List<FeatureOptionViewModel> Features { get; set; } = new();
    
    public List<UpdatePropertyImageDto> PropertyImages { get; set; }
    
    public List<IFormFile> NewImages { get; set; } = new List<IFormFile>();
}