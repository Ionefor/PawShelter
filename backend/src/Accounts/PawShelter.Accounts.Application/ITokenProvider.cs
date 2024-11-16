using PawShelter.Accounts.Domain;

namespace PawShelter.Accounts.Application;

public interface ITokenProvider
{
    string GenerateAccessToken(User user);
}