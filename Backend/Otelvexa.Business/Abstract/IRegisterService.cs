using Microsoft.AspNetCore.Identity;
using Otelvexa.Dto.AuthDtos;

namespace Otelvexa.Business.Abstract;

public interface IRegisterService
{
    Task<IdentityResult> Register(RegisterDto registerDto);
}