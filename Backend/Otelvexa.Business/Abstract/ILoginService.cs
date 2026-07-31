using Otelvexa.Dto.AuthDtos;

namespace Otelvexa.Business.Abstract;

public interface ILoginService
{
    Task<string> Login(LoginDto loginDto);
}