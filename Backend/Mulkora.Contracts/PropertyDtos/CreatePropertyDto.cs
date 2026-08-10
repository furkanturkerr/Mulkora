namespace Mulkora.Dto.PropertyDtos;

public class CreatePropertyDto
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string City { get; set; } = string.Empty;

    public string District { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public int RoomCount { get; set; }

    public int LivingRoomCount { get; set; }

    public int BathroomCount { get; set; }

    public int GrossSquareMeter { get; set; }

    public int NetSquareMeter { get; set; }

    public int BuildingAge { get; set; }

    public int FloorNumber { get; set; }

    public int TotalFloor { get; set; }

    public bool IsFurnished { get; set; }

    public bool IsFeatured { get; set; }

    public int ListingType { get; set; }
    
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedDate { get; set; }

    public int AgentId { get; set; }

    public int CategoryId { get; set; }
    
}