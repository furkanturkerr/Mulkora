namespace Mulkora.Entity.Concrete;

public class Agent
{
    public int AgentId { get; set; }

    public string AppUserId { get; set; } = string.Empty;

    public string Title { get; set; } = "Gayrimenkul Danışmanı";

    public string About { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string District { get; set; } = string.Empty;

    public string? OfficeName { get; set; }

    public string? LicenseNumber { get; set; }

    public string? ImageUrl { get; set; }

    public int ExperienceYear { get; set; }

    public bool IsVerified { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public AppUser AppUser { get; set; } = null!;
    
    public ICollection<Property> Properties { get; set; } = new List<Property>();
}