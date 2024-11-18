using PawShelter.Accounts.Domain;

namespace PawShelter.Accounts.Application;

public interface ITokenProvider
{
    Task<string> GenerateAccessToken(User user, CancellationToken cancellationToken);
}