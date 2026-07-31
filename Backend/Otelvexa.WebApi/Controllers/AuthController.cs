using Microsoft.AspNetCore.Mvc;
using Otelvexa.Business.Abstract;
using Otelvexa.Dto.AuthDtos;

namespace Otelvexa.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRegisterService _registerService;
        private readonly ILoginService _loginService;

        public AuthController(IRegisterService registerService, ILoginService loginService)
        {
            _registerService = registerService;
            _loginService = loginService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await _registerService.Register(registerDto);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);
                return BadRequest(errors);
            }

            return StatusCode(StatusCodes.Status201Created);
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var token = await _loginService.Login(loginDto);
            return Ok(token);
        }
    }
}
