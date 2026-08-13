using Mulkora.Entity.Enums;

namespace Mulkora.Entity.Concrete;

public class Property
{
    public int PropertyId { get; set; }

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

    public ListingType ListingType { get; set; }

    public PropertyStatus Status { get; set; } = PropertyStatus.Draft;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedDate { get; set; }

    public int AgentId { get; set; }

    public Agent Agent { get; set; } = null!;

    public int CategoryId { get; set; }
    
    public Category Category { get; set; } = null!;

    public List<PropertyImage> PropertyImages { get; set; } = new List<PropertyImage>();
    
    public ICollection<Feature> Features { get; set; } = new List<Feature>();
}