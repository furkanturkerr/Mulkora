namespace Mulkora.Dto.AgentDtos;

public class ResultAgentDto
{
    public int AgentId { get; set; }
    public string AppUserId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;
    public string About { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;

    public string? OfficeName { get; set; }
    public string? LicenseNumber { get; set; }
    public string? ImageUrl { get; set; }

    public int ExperienceYear { get; set; }
    public bool IsVerified { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }

    public string FullName => $"{Name} {Surname}";
    public string Location => string.Join(" ", new[] { City, District }.Where(x => !string.IsNullOrWhiteSpace(x)));
}