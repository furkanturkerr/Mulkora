using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mulkora.Dto.AuthDtos;

namespace Mulkora.WebUI.Controllers;

[AutoValidateAntiforgeryToken]
public class AccountController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public AccountController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Default");
        }

        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Default");
        }
        
        if (!ModelState.IsValid)
            return View(dto);
        
        var client = _httpClientFactory.CreateClient("MulkoraApi");
        
        var response = await client.PostAsJsonAsync("api/Auth/login", dto);
        
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();

            ModelState.AddModelError("", string.IsNullOrWhiteSpace(error) ? "E-posta veya şifre hatalı." : error);
            return View(dto);
        }
        
        var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();

        if (result == null || !result.Succeeded || string.IsNullOrWhiteSpace(result.Token))
        {
            ModelState.AddModelError("", "Giriş işlemi tamamlanamadı.");
            return View(dto);
        }
        
        var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(result.Token);
        
        var claims = jwtToken.Claims.ToList();
        
        claims.Add(new Claim("access_token", result.Token));

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme,
            ClaimTypes.Name,
            ClaimTypes.Role);

        var principal = new ClaimsPrincipal(identity);
        
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
            new AuthenticationProperties
            {
                IsPersistent = dto.RememberMe,
                ExpiresUtc = new DateTimeOffset(jwtToken.ValidTo),
                AllowRefresh = false
            });
        
        return RedirectToAction("Index", "Default");
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Default");
        }

        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Default");
        }
        
        if (!ModelState.IsValid)
            return View(dto);

        var client = _httpClientFactory.CreateClient("MulkoraApi");
        var response = await client.PostAsJsonAsync("api/Auth/Register", dto);
        
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();

            ModelState.AddModelError(
                "",
                string.IsNullOrWhiteSpace(error)
                    ? "Kayıt işlemi tamamlanamadı."
                    : error);

            return View(dto);
        }

        return RedirectToAction(nameof(RegisterConfirmation));
    }
    
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        if (string.IsNullOrWhiteSpace(userId) ||
            string.IsNullOrWhiteSpace(token))
        {
            return View("ConfirmEmailFailed");
        }

        var client = _httpClientFactory.CreateClient("MulkoraApi");

        var url =
            "api/Auth/confirm-email" +
            $"?userId={Uri.EscapeDataString(userId)}" +
            $"&token={Uri.EscapeDataString(token)}";

        var response = await client.GetAsync(url);

        if (!response.IsSuccessStatusCode) return View("ConfirmEmailFailed");

        return View("ConfirmEmailSuccess");
    }
    
    [AllowAnonymous]
    [HttpGet]
    public IActionResult RegisterConfirmation()
    {
        return View();
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult ForgotPassword()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Default");
        }

        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        var client = _httpClientFactory.CreateClient("MulkoraApi");

        var response = await client.PostAsJsonAsync("api/Auth/forgot-password", dto);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();

            ModelState.AddModelError(
                "",
                string.IsNullOrWhiteSpace(error)
                    ? "İşlem sırasında bir hata oluştu."
                    : error);

            return View(dto);
        }

        return RedirectToAction(nameof(ForgotPasswordConfirmation));
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult ForgotPasswordConfirmation()
    {
        return View();
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult ResetPassword(string email, string token)
    {
        if (string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(token))
        {
            return RedirectToAction("Login");
        }

        var dto = new ResetPasswordDto
        {
            Email = email,
            Token = token
        };

        return View(dto);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        var client = _httpClientFactory.CreateClient("MulkoraApi");

        var response = await client.PostAsJsonAsync("api/Auth/reset-password", dto);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();

            ModelState.AddModelError(
                "",
                string.IsNullOrWhiteSpace(error)
                    ? "Şifre yenilenemedi."
                    : error);

            return View(dto);
        }

        return RedirectToAction(nameof(ResetPasswordConfirmation));
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult ResetPasswordConfirmation()
    {
        return View();
    }
    
    [AllowAnonymous]
    [HttpGet]
    public IActionResult ResendConfirmationEmail()
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Default");

        return View();
    }
    
    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResendConfirmationEmail(ResendConfirmationEmailDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        var client = _httpClientFactory.CreateClient("MulkoraApi");

        var response = await client.PostAsJsonAsync("api/Auth/resend-confirmation-email", dto);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();

            ModelState.AddModelError(
                "",
                string.IsNullOrWhiteSpace(error)
                    ? "Doğrulama e-postası gönderilemedi."
                    : error);

            return View(dto);
        }


        return View("ResendConfirmationEmailSent");
    }
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction(nameof(Login));
    }
}