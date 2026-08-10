using System.Net;
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

        try
        {
            await SendConfirmationEmailAsync(appUser);
        }
        catch (Exception e)
        {
            await _userManager.DeleteAsync(appUser);
            Console.WriteLine(e);
            throw;
        }

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
            await _userManager.AddToRoleAsync(user, "CUSTOMERS");
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

    public async Task ResendConfirmationEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            return;

        if (await _userManager.IsEmailConfirmedAsync(user))
            return;

        await SendConfirmationEmailAsync(user);
    }

    private async Task SendConfirmationEmailAsync(AppUser user)
    {
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        var frontendUrl = _configuration["FrontendUrl"]
                          ?? throw new InvalidOperationException(
                              "FrontendUrl bulunamadı.");

        var confirmationLink =
            $"{frontendUrl.TrimEnd('/')}/Account/ConfirmEmail" +
            $"?userId={Uri.EscapeDataString(user.Id)}" +
            $"&token={Uri.EscapeDataString(encodedToken)}";

        var safeName = WebUtility.HtmlEncode(user.Name);
        var safeLink = WebUtility.HtmlEncode(confirmationLink);

        var htmlBody = $$"""
                         <!doctype html>
                         <html lang="tr">
                         <head>
                             <meta charset="utf-8">
                             <meta name="viewport" content="width=device-width">
                             <title>E-posta doğrulama</title>
                         </head>

                         <body style="margin:0;padding:0;background:#f1f2ed;font-family:Arial,Helvetica,sans-serif;">

                         <table width="100%"
                                cellpadding="0"
                                cellspacing="0"
                                role="presentation"
                                style="background:#f1f2ed;">

                             <tr>
                                 <td align="center"
                                     style="padding:44px 16px;">

                                     <table width="100%"
                                            cellpadding="0"
                                            cellspacing="0"
                                            role="presentation"
                                            style="max-width:620px;background:#ffffff;border:1px solid #dfe6e2;border-radius:24px;overflow:hidden;">

                                         <tr>
                                             <td style="padding:30px 38px;background:#123f35;">

                                                 <div style="font-size:27px;font-weight:800;color:#ffffff;letter-spacing:-1px;">
                                                     Mülk<span style="color:#d8b078;">ora</span>
                                                 </div>

                                                 <div style="margin-top:7px;font-size:13px;color:#c4d4cf;">
                                                     Doğru evi keşfedin, randevunuzu kolayca oluşturun.
                                                 </div>

                                             </td>
                                         </tr>

                                         <tr>
                                             <td style="padding:42px 38px 36px;">

                                                 <table cellpadding="0"
                                                        cellspacing="0"
                                                        role="presentation">

                                                     <tr>
                                                         <td align="center"
                                                             style="width:58px;height:58px;border-radius:18px;background:#e8f2ee;color:#178268;font-size:25px;">

                                                             ✉
                                                         </td>
                                                     </tr>

                                                 </table>

                                                 <h1 style="margin:26px 0 16px;color:#14201c;font-size:30px;line-height:1.25;letter-spacing:-.7px;">
                                                     E-posta adresinizi doğrulayın
                                                 </h1>

                                                 <p style="margin:0 0 12px;color:#5d6b66;font-size:16px;line-height:1.7;">
                                                     Merhaba {{safeName}},
                                                 </p>

                                                 <p style="margin:0 0 28px;color:#5d6b66;font-size:16px;line-height:1.7;">
                                                     Mülkora hesabınız başarıyla oluşturuldu.
                                                     Hesabınızı etkinleştirmek ve ilanları favorilerinize
                                                     eklemeye başlamak için aşağıdaki butona tıklayın.
                                                 </p>

                                                 <table cellpadding="0"
                                                        cellspacing="0"
                                                        role="presentation">

                                                     <tr>
                                                         <td style="border-radius:12px;background:#174c40;">

                                                             <a href="{{safeLink}}"
                                                                target="_blank"
                                                                style="display:inline-block;padding:16px 28px;color:#ffffff;text-decoration:none;font-size:15px;font-weight:700;">

                                                                 E-posta adresimi doğrula
                                                             </a>

                                                         </td>
                                                     </tr>

                                                 </table>

                                                 <table width="100%"
                                                        cellpadding="0"
                                                        cellspacing="0"
                                                        role="presentation"
                                                        style="margin-top:30px;">

                                                     <tr>
                                                         <td style="padding:17px 18px;border:1px solid #e2e9e6;border-radius:13px;background:#f7faf8;color:#65726e;font-size:13px;line-height:1.65;">

                                                             Bu doğrulama isteğini siz oluşturmadıysanız
                                                             herhangi bir işlem yapmanıza gerek yoktur.

                                                         </td>
                                                     </tr>

                                                 </table>

                                                 <p style="margin:28px 0 8px;color:#8a9692;font-size:12px;line-height:1.6;">
                                                     Buton çalışmazsa aşağıdaki bağlantıyı kopyalayıp
                                                     tarayıcınızın adres çubuğuna yapıştırabilirsiniz:
                                                 </p>

                                                 <p style="margin:0;word-break:break-all;">

                                                     <a href="{{safeLink}}"
                                                        style="color:#178268;font-size:12px;line-height:1.6;">

                                                         {{safeLink}}
                                                     </a>

                                                 </p>

                                             </td>
                                         </tr>

                                         <tr>
                                             <td style="padding:22px 38px;background:#f7faf8;border-top:1px solid #e5ebe8;color:#89948f;font-size:12px;line-height:1.6;">

                                                 © {{DateTime.UtcNow.Year}} Mülkora<br>
                                                 Bu e-posta otomatik olarak gönderilmiştir.

                                             </td>
                                         </tr>

                                     </table>

                                 </td>
                             </tr>

                         </table>

                         </body>
                         </html>
                         """;

        await _emailService.SendEmailAsync(
            user.Email!,
            "Mülkora | E-posta adresinizi doğrulayın",
            htmlBody);
    }
}