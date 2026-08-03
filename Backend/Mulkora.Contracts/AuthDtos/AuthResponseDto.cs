namespace Mulkora.Dto.AuthDtos;

public class AuthResponseDto
{
    public bool Succeeded { get; set; }
    public string Token { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool EmailConfirmed { get; set; }
}