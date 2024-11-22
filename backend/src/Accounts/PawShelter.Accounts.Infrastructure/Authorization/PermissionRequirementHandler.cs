﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using PawShelter.Accounts.Contracts;
using PawShelter.Core.Models;

namespace PawShelter.Accounts.Infrastructure.Authorization;

public class PermissionRequirementHandler : AuthorizationHandler<PermissionAttribute>
{
    private readonly IServiceScopeFactory _scopeFactory;
    
    public PermissionRequirementHandler(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionAttribute permission)
    {
        using var scope = _scopeFactory.CreateScope();
        
        var accountContract = scope.ServiceProvider.GetRequiredService<IAccountsContract>();
        
        var userIdString = context.User.Claims
            .FirstOrDefault(c => c.Type == CustomClaims.Id)?.Value;
        
        if (!Guid.TryParse(userIdString, out Guid userId))
        {
            context.Fail();
            return;
        }

        var permissions = await accountContract.GetUserPermissionsCodes(userId);

        if (permissions.Contains(permission.Code))
        {
            context.Succeed(permission);
            return;
        }

        context.Fail();
    }
}