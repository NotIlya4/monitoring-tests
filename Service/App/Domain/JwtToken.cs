using System.IdentityModel.Tokens.Jwt;

namespace Service.Domain;

public record JwtToken
{
    public string Token { get; }
    public JwtSecurityToken JwtSecurityToken { get; }
    public string Login => (string)JwtSecurityToken.Payload["login"];

    public JwtToken(string token)
    {
        Token = token;
        JwtSecurityToken = new JwtSecurityToken(token);
    }

    public JwtToken(JwtSecurityToken token)
    {
        Token = token.RawData;
        JwtSecurityToken = token;
    }

    public static implicit operator string(JwtToken token)
    {
        return token.Token;
    }

    public static implicit operator JwtToken(string rawToken)
    {
        return new JwtToken(rawToken);
    }
}