using CSharpFunctionalExtensions;
using PawShelter.Accounts.Domain;
using PawShelter.SharedKernel;

namespace PawShelter.Accounts.Application.Abstractions;

public interface IRefreshSessionManager
{
    Task<Result<RefreshSession, Error>> GetByRefreshToken(Guid refreshToken);

    void Delete(RefreshSession refreshSession);
}