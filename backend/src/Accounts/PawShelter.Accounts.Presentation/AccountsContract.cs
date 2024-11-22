using PawShelter.Accounts.Application.Abstractions;
using PawShelter.Accounts.Contracts;

namespace PawShelter.Accounts.Presentation;

public class AccountsContract : IAccountsContract
{
    private readonly IPermissionManager _permissionManager;
    public AccountsContract(IPermissionManager permissionManager)
    {
        _permissionManager = permissionManager;
    }
    public Task<HashSet<string>> GetUserPermissionsCodes(Guid userId)
    {
        return _permissionManager.GetUserPermissions(userId);
    }
}