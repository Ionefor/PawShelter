using PawShelter.Accounts.Contracts;
using PawShelter.Accounts.Infrastructure.Seading;

namespace PawShelter.Accounts.Presentation;

public class AccountsContract : IAccountsContract
{
    private readonly PermissionManager _permissionManager;

    public AccountsContract(PermissionManager permissionManager)
    {
        _permissionManager = permissionManager;
    }
    
    public Task<HashSet<string>> GetUserPermissionsCodes(Guid userId)
    {
        return _permissionManager.GetUserPermissions(userId);
    }
}