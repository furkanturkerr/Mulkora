using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Mulkora.Business.Abstract;
using Mulkora.Dto.AuthDtos;
using Mulkora.Entity.Concrete;

namespace Mulkora.Business.Manager;

public class RegisterService : IRegisterService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;

    public RegisterService(UserManager<AppUser> userManager, IConfiguration configuration, IEmailService emailService)
    {
        _userManager = userManager;
        _configuration = configuration;
        _emailService = emailService;
    }

    public async Task<IdentityResult> Register(RegisterDto registerDto)
    {
        AppUser appUser = new AppUser
        {
            Name = registerDto.Name,
            Surname = registerDto.Surname,
            UserName = registerDto.Email,
            Email = registerDto.Email
        };

        var result = await _userManager.CreateAsync(appUser, registerDto.Password);

        if (!result.Succeeded)
            return result;
        
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        var frontendUrl = _configuration["FrontendUrl"] ?? throw new InvalidOperationException("FrontendUrl bulunamadı.");

        var confirmationLink =
            $"{frontendUrl.TrimEnd('/')}/Account/ConfirmEmail" +
            $"?userId={Uri.EscapeDataString(appUser.Id)}" +
            $"&token={Uri.EscapeDataString(encodedToken)}";
        
        var htmlBody = $"""
<!doctype html>
<html lang="tr">
<body style="margin:0;padding:0;background:#f2f1eb;font-family:Arial,sans-serif;">

<table width="100%"
       cellpadding="0"
       cellspacing="0"
       role="presentation"
       style="background:#f2f1eb;padding:40px 16px;">

    <tr>
        <td align="center">

            <table width="100%"
                   cellpadding="0"
                   cellspacing="0"
                   role="presentation"
                   style="max-width:600px;background:#ffffff;border-radius:22px;overflow:hidden;border:1px solid #e1e7e4;">

                <tr>
                    <td style="padding:26px 34px;background:#123f35;color:#ffffff;">

                        <div style="font-size:24px;font-weight:800;">
                            Mülk<span style="color:#d5ae78;">ora</span>
                        </div>

                        <div style="margin-top:5px;font-size:13px;color:#c5d5d0;">
                            Doğru evi bulun, randevunuzu oluşturun.
                        </div>

                    </td>
                </tr>

                <tr>
                    <td style="padding:42px 34px;">

                        <div style="width:54px;height:54px;line-height:54px;text-align:center;border-radius:16px;background:#e8f2ee;color:#178268;font-size:24px;">
                            ✉
                        </div>

                        <h1 style="margin:25px 0 15px;color:#14201c;font-size:29px;line-height:1.25;">
                            E-posta adresinizi doğrulayın
                        </h1>

                        <p style="margin:0 0 12px;color:#5f6e69;font-size:16px;line-height:1.7;">
                            Merhaba {appUser.Name},
                        </p>

                        <p style="margin:0 0 28px;color:#5f6e69;font-size:16px;line-height:1.7;">
                            Mülkora hesabınız başarıyla oluşturuldu.
                            Hesabınızı etkinleştirmek için aşağıdaki
                            butona tıklayın.
                        </p>

                        <table cellpadding="0"
                               cellspacing="0"
                               role="presentation">

                            <tr>
                                <td style="border-radius:12px;background:#174c40;">

                                    <a href="{confirmationLink}"
                                       style="display:inline-block;padding:15px 26px;color:#ffffff;text-decoration:none;font-size:15px;font-weight:700;">

                                        E-posta adresimi doğrula
                                    </a>

                                </td>
                            </tr>

                        </table>

                        <div style="margin-top:30px;padding:16px 18px;border-radius:12px;background:#f7faf8;color:#66736f;font-size:13px;line-height:1.65;">
                            Bu hesabı siz oluşturmadıysanız bu e-postayı
                            dikkate almanıza gerek yoktur.
                        </div>

                        <p style="margin:26px 0 8px;color:#89938f;font-size:12px;">
                            Buton çalışmazsa aşağıdaki bağlantıyı tarayıcınıza yapıştırın:
                        </p>

                        <p style="margin:0;word-break:break-all;">

                            <a href="{confirmationLink}"
                               style="color:#178268;font-size:12px;line-height:1.6;">

                                {confirmationLink}
                            </a>

                        </p>

                    </td>
                </tr>

                <tr>
                    <td style="padding:20px 34px;background:#f7faf8;color:#8a9692;font-size:12px;">
                        © {DateTime.UtcNow.Year} Mülkora
                    </td>
                </tr>

            </table>

        </td>
    </tr>

</table>

</body>
</html>
""";

        await _emailService.SendEmailAsync(appUser.Email!, "Mülkora | E-posta doğrulama", htmlBody);

        return result;
    }

    public async Task<IdentityResult> ConfirmEmailAsync(string userId, string token)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Description = "Doğrulama bağlantısı geçersiz."
            });
        }

        try
        {
            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            return await _userManager.ConfirmEmailAsync(user, decodedToken);
        }
        catch
        {
            return IdentityResult.Failed(new IdentityError
            {
                Description = "Doğrulama bağlantısı geçersiz."
            });
        }
    }
}