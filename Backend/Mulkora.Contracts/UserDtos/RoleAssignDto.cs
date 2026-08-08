namespace Mulkora.Dto.UserDtos;

public class RoleAssignDto
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public List<RoleAssignItemDto> Roles { get; set; } = new();
}