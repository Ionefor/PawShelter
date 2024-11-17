using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PawShelter.Accounts.Domain;
using PawShelter.Accounts.Infrastructure.Options;
using PawShelter.Core.Models;

namespace PawShelter.Accounts.Infrastructure.Seading;

public class AccountsSeederService(
    RoleManager<Role> roleManager,
    PermissionManager permissionManager,
    RolePermissionManager rolePermissionManager,
    ILogger<AccountSeeder> logger)
{
    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        var json = await File.ReadAllTextAsync(FilePath.Accounts, cancellationToken);

        var seedData = JsonSerializer.Deserialize<RolePermissionOptions>(json)
                       ?? throw new ApplicationException("Could not deserialize role permission config");

        await SeedPermissions(seedData);

        await SeedRoles(seedData);

        await SeedRolePermissions(seedData);
        
    }
    
    private async Task SeedPermissions(RolePermissionOptions seedData)
    {
        var permissionsToAdd = seedData.Permissions.
            SelectMany(permissions => permissions.Value);

        await permissionManager.AddRangeIfExist(permissionsToAdd);
        
        logger.LogInformation("Permissions added to seed data");
    }
    
    private async Task SeedRoles(RolePermissionOptions seedData)
    {
        foreach (var role in seedData.Roles.Keys)
        {
            var existingRole = await roleManager.FindByNameAsync(role);

            if (existingRole is null)
            {
                await roleManager.CreateAsync(new Role { Name = role });
            }
        }
        
        logger.LogInformation("Role added to database.");
    }

    public async Task SeedRolePermissions(RolePermissionOptions seedData)
    {
        foreach (var roleName in seedData.Roles.Keys)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            
            var rolePermissions = seedData.Roles[roleName];
            
            await rolePermissionManager.AddRangeIfExist(role!.Id, rolePermissions);
        }
        
        logger.LogInformation("Role permissions added to database.");
    }
}