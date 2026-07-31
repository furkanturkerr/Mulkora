using Microsoft.AspNetCore.Identity;
using Mulkora.Dto.AuthDtos;

namespace Mulkora.Business.Abstract;

public interface IRegisterService
{
    Task<IdentityResult> Register(RegisterDto registerDto);
}