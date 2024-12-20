﻿using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PawShelter.Accounts.Application.Abstractions;
using PawShelter.Accounts.Domain;
using PawShelter.Accounts.Infrastructure.DbContexts;
using PawShelter.SharedKernel.Models.Error;

namespace PawShelter.Accounts.Infrastructure.Authorization;

public class RefreshSessionManager(AccountsWriteDbContext accountsWriteDbContext) : IRefreshSessionManager
{
    public async Task<Result<RefreshSession, Error>> GetByRefreshToken(Guid refreshToken)
    {
        var token =  await accountsWriteDbContext.RefreshSessions.
            Include(s => s.User).
            FirstOrDefaultAsync(r => r.RefreshToken == refreshToken);

        if (token is null)
        {
            return Errors.General.NotFound(
                new ErrorParameters.General.NotFound(
                    nameof(RefreshSession), nameof(refreshToken), refreshToken));
        }

        return token;
    }
    
    public void Delete(RefreshSession refreshSession)
    {
        accountsWriteDbContext.Remove(refreshSession);
    }
}