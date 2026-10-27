using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Lab10_MaxiFranco.Application.Interfaces.Services;
using Lab10_MaxiFranco.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
namespace Lab10_MaxiFranco.Infrastructure.Security;
public class JwtTokenService(IConfiguration config) : ITokenService
{
    public (string Token, DateTime ExpiresAt) Generate(User user, IEnumerable<string> roles)
    {
        var key = config["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key no configurada.");
        if (Encoding.UTF8.GetByteCount(key) < 32) throw new InvalidOperationException("Jwt:Key requiere al menos 32 bytes.");
        var issuer = config["Jwt:Issuer"] ?? "TicketeraApi";
        var audience = config["Jwt:Audience"] ?? "TicketeraClients";
        var claims = new List<Claim> { new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new(ClaimTypes.NameIdentifier, user.UserId.ToString()), new(ClaimTypes.Name, user.Username),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) };
        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
        var expires = DateTime.UtcNow.AddHours(2);
        var token = new JwtSecurityToken(issuer, audience, claims, expires: expires,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256));
        return (new JwtSecurityTokenHandler().WriteToken(token), expires);
    }
}
