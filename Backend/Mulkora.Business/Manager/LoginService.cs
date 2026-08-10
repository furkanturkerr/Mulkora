using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Mulkora.Business.Abstract;
using Mulkora.DataAccess.Abstract;
using Mulkora.Dto.AuthDtos;
using Mulkora.Entity.Concrete;

namespace Mulkora.Business.Manager;

public class LoginService : ILoginService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IAgentDal _agentDal;


    public LoginService(UserManager<AppUser> userManager, IConfiguration configuration, IAgentDal agentDal)
    {
        _userManager = userManager;
        _configuration = configuration;
        _agentDal = agentDal;
    }

    public async Task<string> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
        {
            throw new UnauthorizedAccessException(
                "E-posta veya şifre hatalı."
            );
        }
        
        if (!await _userManager.IsEmailConfirmedAsync(user))
        {
            throw new UnauthorizedAccessException(
                "Giriş yapmadan önce e-posta adresinizi doğrulayın.");
        }

        return await GenerateToken(user);
    }

    private async Task<string> GenerateToken(AppUser user)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Surname, user.Surname)
        };
        
        var agent = await _agentDal.GetByUserIdAsync(user.Id);

        if (agent != null)
        {
            claims.Add(new Claim("AgentId", agent.AgentId.ToString()));
        }

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpireMinutes"]!)),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}