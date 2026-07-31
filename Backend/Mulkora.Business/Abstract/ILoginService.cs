using Mulkora.Dto.AuthDtos;

namespace Mulkora.Business.Abstract;

public interface ILoginService
{
    Task<string> Login(LoginDto loginDto);
}