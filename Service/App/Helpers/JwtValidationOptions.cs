namespace Service.Helpers;

public record JwtValidationOptions(string Secret, TimeSpan ExpireIn) { }