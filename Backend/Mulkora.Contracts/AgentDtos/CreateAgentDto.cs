namespace Mulkora.Dto.AgentDtos;

public class CreateAgentDto
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Title { get; set; }
    public string? About { get; set; }
    public string? City { get; set; }
    public string? District { get; set; }
    public string? OfficeName { get; set; }
    public string? LicenseNumber { get; set; }
    public string? ImageUrl { get; set; }
    public int ExperienceYear { get; set; }
    public bool IsVerified { get; set; }
    public bool IsActive { get; set; }
}