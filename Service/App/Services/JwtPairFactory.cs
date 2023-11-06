using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Internal;
using Microsoft.IdentityModel.Tokens;
using Service.Domain;
using Service.Helpers;

namespace Service.Services;

public interface IJwtPairFactory
{
    public Guid CreateRefreshToken();
    public JwtToken CreateJwtToken(string login);
}

public class JwtPairFactory : IJwtPairFactory
{
    private readonly ISystemClock _clock;
    private readonly JwtValidationOptions _jwtValidationOptions;
    private readonly JwtSecurityTokenHandler _handler = new();
    
    public JwtPairFactory(ISystemClock clock, JwtValidationOptions options)
    {
        _clock = clock;
        _jwtValidationOptions = options;
    }
    
    public Guid CreateRefreshToken()
    {
        return Guid.NewGuid();
    }

    public JwtToken CreateJwtToken(string login)
    {
        var claims = new List<Claim>()
        {
            new("login", login),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var currentTime = _clock.UtcNow.UtcDateTime;
        var expiresAt = currentTime.Add(_jwtValidationOptions.ExpireIn);
        
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            IssuedAt = currentTime,
            Expires = expiresAt,
            NotBefore = expiresAt.Subtract(TimeSpan.FromSeconds(1)),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtValidationOptions.Secret)),
                    SecurityAlgorithms.HmacSha256)
        };

        return new JwtToken(_handler.CreateJwtSecurityToken(tokenDescriptor));
    }
}