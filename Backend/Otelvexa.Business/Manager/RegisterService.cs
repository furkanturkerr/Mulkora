using Microsoft.AspNetCore.Identity;
using Otelvexa.Business.Abstract;
using Otelvexa.Dto.AuthDtos;
using Otelvexa.Entity.Concrete;

namespace Otelvexa.Business.Manager;

public class RegisterService : IRegisterService
{
    private readonly UserManager<AppUser> _userManager;

    public RegisterService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
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
        return result;
    }
}