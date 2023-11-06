namespace Service.Domain;

public record JwtPair(JwtToken JwtToken, Guid RefreshToken) { }