using Microsoft.AspNetCore.Mvc;
using Mulkora.Business.Abstract;
using Mulkora.Dto.AuthDtos;

namespace Mulkora.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRegisterService _registerService;
        private readonly ILoginService _loginService;
        private readonly IPasswordResetService _passwordResetService;

        public AuthController(IRegisterService registerService, ILoginService loginService, IPasswordResetService passwordResetService)
        {
            _registerService = registerService;
            _loginService = loginService;
            _passwordResetService = passwordResetService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await _registerService.Register(registerDto);

            if (!result.Succeeded)
            {
                var errors = result.Errors
                    .Select(x => x.Description)
                    .Distinct();

                foreach (var error in errors)
                {
                    ModelState.AddModelError("Register", error);
                }

                return ValidationProblem(ModelState);
            }

            return StatusCode(StatusCodes.Status201Created);
        }
        
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var result = await _registerService.ConfirmEmailAsync(userId, token);

            if (!result.Succeeded) return BadRequest("Doğrulama bağlantısı geçersiz veya süresi dolmuş.");

            return Ok();
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var token = await _loginService.Login(loginDto);
            return Ok(new AuthResponseDto
            {
                Succeeded = true,
                Token = token,
                Message = "Giriş başarılı."
            });
        }
        
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            await _passwordResetService.ForgotPasswordAsync(dto.Email);

            return Ok(new
            {
                message =
                    "E-posta kayıtlıysa şifre sıfırlama bağlantısı gönderildi."
            });
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            await _passwordResetService.ResetPasswordAsync(dto);

            return Ok(new
            {
                message = "Şifreniz başarıyla yenilendi."
            });
        }
    }
}
