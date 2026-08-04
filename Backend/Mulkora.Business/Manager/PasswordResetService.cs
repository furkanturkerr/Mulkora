using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Mulkora.Business.Abstract;
using Mulkora.Dto.AuthDtos;
using Mulkora.Entity.Concrete;

namespace Mulkora.Business.Manager;

public class PasswordResetService : IPasswordResetService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;

    public PasswordResetService(UserManager<AppUser> userManager, IEmailService emailService, IConfiguration configuration)
    {
        _userManager = userManager;
        _emailService = emailService;
        _configuration = configuration;
    }

    public async Task ForgotPasswordAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        
        if (user == null)
            return;

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //Bu işlem tokenı şifrelemez. Yalnızca URL’ye uygun biçime dönüştürür.
        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        
        var frontendUrl = _configuration["FrontendUrl"]
                          ?? throw new InvalidOperationException(
                              "FrontendUrl ayarı bulunamadı.");
        
        var resetLink =
            $"{frontendUrl}/Account/ResetPassword" +
            $"?email={Uri.EscapeDataString(email)}" +
            $"&token={Uri.EscapeDataString(encodedToken)}";
        
        var htmlBody = $"""
                        <!doctype html>
                        <html lang="tr">
                        <body style="margin:0;background:#f4f6f5;font-family:Arial,sans-serif;">
                            <div style="max-width:560px;margin:40px auto;background:#fff;
                                        border:1px solid #e5e7eb;border-radius:16px;overflow:hidden;">

                                <div style="background:#123b32;padding:24px 32px;color:white;">
                                    <h2 style="margin:0;">Mülkora</h2>
                                </div>

                                <div style="padding:32px;">
                                    <h2 style="margin-top:0;color:#17211e;">Şifrenizi yenileyin</h2>

                                    <p style="color:#66736f;line-height:1.6;">
                                        Merhaba {user.Name}, hesabınız için şifre sıfırlama
                                        isteği aldık.
                                    </p>

                                    <a href="{resetLink}"
                                       style="display:inline-block;margin-top:12px;padding:14px 24px;
                                              background:#238268;color:white;text-decoration:none;
                                              border-radius:10px;font-weight:bold;">
                                        Şifremi yenile
                                    </a>

                                    <p style="margin-top:28px;color:#89938f;font-size:13px;">
                                        Bu isteği siz oluşturmadıysanız e-postayı dikkate almayın.
                                    </p>
                                </div>
                            </div>
                        </body>
                        </html>
                        """;

        await _emailService.SendEmailAsync(
            user.Email!,
            "Mülkora Şifre Sıfırlama",
            htmlBody);
        
    }

    public async Task ResetPasswordAsync(ResetPasswordDto dto)
    {
        if (dto.NewPassword != dto.ConfirmPassword)
            throw new Exception("Yeni şifreler birbiriyle eşleşmiyor.");

        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user == null)
            throw new Exception("Şifre sıfırlama bağlantısı geçersiz.");

        string decodedToken;

        try
        {
            decodedToken = Encoding.UTF8.GetString(
                WebEncoders.Base64UrlDecode(dto.Token));
        }
        catch
        {
            throw new Exception("Şifre sıfırlama bağlantısı geçersiz.");
        }

        var result = await _userManager.ResetPasswordAsync(
            user,
            decodedToken,
            dto.NewPassword);

        if (!result.Succeeded)
        {
            var errors = string.Join(" ", result.Errors
                .Select(x => x.Description)
                .Distinct());

            throw new Exception(errors);
        }
    }
}