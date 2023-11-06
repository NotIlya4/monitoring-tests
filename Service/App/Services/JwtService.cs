using Microsoft.IdentityModel.Tokens;
using Service.Domain;
using Service.Helpers;

namespace Service.Services;

public interface IJwtService
{
    public JwtPair RefreshToken(JwtPair oldPair);
    public void ValidateJwtToken(JwtToken token, string login);
}

public class JwtInMemoryService : IJwtService
{
    private readonly RefreshValidationOptions _refreshValidationOptions;
    private readonly IJwtPairFactory _factory;
    private readonly TokenValidationParameters 
    
    public JwtInMemoryService(RefreshValidationOptions refreshValidationOptions, IJwtPairFactory factory)
    {
        _refreshValidationOptions = refreshValidationOptions;
        _factory = factory;
    }
    
    public JwtPair RefreshToken(JwtPair oldPair)
    {
        throw new Exception();
    }

    public void ValidateJwtToken(JwtToken token, string login)
    {
        
    }
}