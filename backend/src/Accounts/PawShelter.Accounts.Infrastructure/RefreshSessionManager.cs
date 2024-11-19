using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PawShelter.Accounts.Application.Abstractions;
using PawShelter.Accounts.Domain;
using PawShelter.Accounts.Infrastructure.DbContexts;
using PawShelter.SharedKernel;

namespace PawShelter.Accounts.Infrastructure;

public class RefreshSessionManager(AccountDbContext accountDbContext) : IRefreshSessionManager
{
    public async Task<Result<RefreshSession, Error>> GetByRefreshToken(Guid refreshToken)
    {
        var token =  await accountDbContext.RefreshSessions.
            Include(s => s.User).
            FirstOrDefaultAsync(r => r.RefreshToken == refreshToken);

        if (token is null)
            return Errors.General.NotFound(refreshToken);

        return token;
    }
    
    public void Delete(RefreshSession refreshSession)
    {
        accountDbContext.Remove(refreshSession);
    }
}