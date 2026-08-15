using Microsoft.AspNetCore.Mvc.Rendering;
using Mulkora.Dto.PropertyDtos;

namespace Mulkora.WebUI.Models;

public class PublicPropertyListViewModel
{
    public List<ResultPropertyDto> Properties { get; set; } = new();

    public List<SelectListItem> Categories { get; set; } = new();

    public string? City { get; set; }

    public string? District { get; set; }

    public int? ListingType { get; set; }

    public int? MaxPrice { get; set; }

    public int? MinPrice { get; set; }

    public int? CategoryId { get; set; }

    public int? RoomCount { get; set; }

    public int CurrentPage { get; set; } = 1;

    public int TotalPages { get; set; }

    public string ApiBaseUrl { get; set; } = string.Empty;
}