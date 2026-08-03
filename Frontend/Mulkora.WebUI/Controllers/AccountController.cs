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
    public IActionResult Login()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
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

        return RedirectToAction(nameof(Login));
    }

    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
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
    public IActionResult ForgotPasswordConfirmation()
    {
        return View();
    }

    [HttpGet]
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
    public IActionResult ResetPasswordConfirmation()
    {
        return View();
    }
}