using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mulkora.Business.Abstract;
using Mulkora.Dto.AuthDtos;

namespace Mulkora.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly ILoginService _loginService;
    private readonly IRegisterService _registerService;
    private readonly IPasswordResetService _passwordResetService;

    public AuthController(
        ILoginService loginService,
        IRegisterService registerService,
        IPasswordResetService passwordResetService)
    {
        _loginService = loginService;
        _registerService = registerService;
        _passwordResetService = passwordResetService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var token = await _loginService.Login(dto);

        return Ok(new AuthResponseDto
        {
            Succeeded = true,
            Token = token,
            Message = "Giriş başarılı."
        });
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var result = await _registerService.Register(dto);

        if (!result.Succeeded)
        {
            var message = string.Join(" ", result.Errors
                .Select(x => x.Description)
                .Distinct());

            throw new Exception(message);
        }

        return NoContent();
    }

    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        var result = await _registerService.ConfirmEmailAsync(userId, token);

        if (!result.Succeeded)
        {
            var message = string.Join(" ", result.Errors
                .Select(x => x.Description)
                .Distinct());

            throw new Exception(message);
        }

        return NoContent();
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
    {
        await _passwordResetService.ForgotPasswordAsync(dto.Email);

        return NoContent();
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
    {
        await _passwordResetService.ResetPasswordAsync(dto);

        return NoContent();
    }

    [HttpPost("resend-confirmation-email")]
    public async Task<IActionResult> ResendConfirmationEmail(ResendConfirmationEmailDto dto)
    {
        await _registerService.ResendConfirmationEmailAsync(dto.Email);

        return NoContent();
    }
}