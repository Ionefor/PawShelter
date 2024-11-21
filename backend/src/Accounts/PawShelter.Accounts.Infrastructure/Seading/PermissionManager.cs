using Microsoft.EntityFrameworkCore;
using PawShelter.Accounts.Application.Abstractions;
using PawShelter.Accounts.Domain;
using PawShelter.Accounts.Infrastructure.DbContexts;

namespace PawShelter.Accounts.Infrastructure.Seading;

public class PermissionManager(AccountDbContext accountDbContext) : IPermissionManager
{
    public async Task AddRangeIfExist(IEnumerable<string> permissions)
    {
        foreach (var permissionCode in permissions)
        {
            var isPermissionExist = await accountDbContext.Permissions.
                AnyAsync(p => p.Code == permissionCode);

            if (!isPermissionExist)
            {
                await accountDbContext.Permissions.
                    AddAsync(new Permission { Code = permissionCode });
            }
        }
        
        await accountDbContext.SaveChangesAsync();
    }
    
    public async Task<HashSet<string>> GetUserPermissions(
        Guid userId, 
        CancellationToken cancellationToken = default)
    {
        var permissions = await accountDbContext.Users
            .Include(u => u.Roles)
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Roles)
            .SelectMany(r=>r.RolePermission)
            .Select(rp=>rp.Permission.Code)
            .ToListAsync(cancellationToken);

        return permissions.ToHashSet();
    }
}