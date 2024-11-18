using System.Security.Claims;
using CSharpFunctionalExtensions;
using PawShelter.Accounts.Domain;
using PawShelter.SharedKernel;

namespace PawShelter.Accounts.Application.Abstractions;

public interface ITokenProvider
{
    Task<JwtTokenResult> GenerateAccessToken(User user, CancellationToken cancellationToken);
    
    Task<Guid> GenerateRefreshToken(User user, Guid accesTokenJti, CancellationToken cancellationToken);

    Task<Result<IReadOnlyList<Claim>, Error>> GetUserClaims(string jwtToken, CancellationToken cancellationToken);
}