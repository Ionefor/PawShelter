namespace PawShelter.Accounts.Application;

public record JwtTokenResult(string AccessToken, Guid Jti);