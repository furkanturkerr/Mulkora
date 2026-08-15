using Mulkora.Dto.PropertyDtos;
using Mulkora.Dto.PropertyImageDtos;

namespace Mulkora.WebUI.Models;

public class PublicPropertyDetailsViewModel
{
    public GetByIdPropertyDto Property { get; set; } = new();

    public List<ResultPropertyImageDto> Images { get; set; } = new();

    public string ApiBaseUrl { get; set; } = string.Empty;
}