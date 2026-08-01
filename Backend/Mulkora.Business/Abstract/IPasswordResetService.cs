using Mulkora.Dto.AuthDtos;

namespace Mulkora.Business.Abstract;

public interface IPasswordResetService
{
    Task ForgotPasswordAsync(string email);
    Task ResetPasswordAsync(ResetPasswordDto dto);
}