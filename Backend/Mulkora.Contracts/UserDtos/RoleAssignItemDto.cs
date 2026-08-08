namespace Mulkora.Dto.UserDtos;

public class RoleAssignItemDto
{
    public string RoleId { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public bool IsSelected { get; set; }
}