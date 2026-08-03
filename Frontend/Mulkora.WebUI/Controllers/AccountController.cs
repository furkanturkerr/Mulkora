using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mulkora.Dto.AuthDtos;
using Mulkora.WebApi.Models;

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
        
        var response = await client.PostAsJsonAsync("http://localhost:5214/api/Auth/login", dto);
        var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
        if (!response.IsSuccessStatusCode || result == null || !result.Succeeded || string.IsNullOrWhiteSpace(result.Token))
        {
            ModelState.AddModelError("", result.Message ?? "E-posta veya şifre hatalı.");
            return View(dto);
        }
        
        var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(result.Token);
        
        var claims = jwtToken.Claims.ToList();
        
        claims.Add(new Claim("access_token", result.Token));

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme,
            "unique_name", "role");

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
        var response = await client.PostAsJsonAsync("http://localhost:5214/api/Auth/Register", dto);
        if (!response.IsSuccessStatusCode)
        {
            var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

            if (problem != null)
            {
                foreach (var errorList in problem.Errors.Values)
                {
                    foreach (var error in errorList)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
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

        var client = _httpClientFactory.CreateClient();

        var response = await client.PostAsJsonAsync("http://localhost:5214/api/Auth/ForgotPassword", dto);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(
                "",
                "İşlem sırasında bir hata oluştu."
            );

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

        var client = _httpClientFactory.CreateClient();

        var response = await client.PostAsJsonAsync("http://localhost:5214/api/Auth/ResetPassword", dto);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();

            ModelState.AddModelError(
                "",
                error?.Message ?? "Şifre yenilenemedi."
            );

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
            ModelState.AddModelError(
                "",
                "Doğrulama e-postası gönderilemedi.");

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