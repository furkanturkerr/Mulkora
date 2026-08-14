using Mulkora.Dto.PropertyDtos;
using Mulkora.Dto.PropertyImageDtos;

namespace Mulkora.WebUI.Areas.Admin.Models;

public class PropertyDetailsViewModel
{
    public GetByIdPropertyDto Property { get; set; } = new();

    public List<UpdatePropertyImageDto> PropertyImages { get; set; } = new();
}