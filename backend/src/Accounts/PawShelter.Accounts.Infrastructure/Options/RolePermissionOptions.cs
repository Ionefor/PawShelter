namespace PawShelter.Accounts.Infrastructure.Options;

public class RolePermissionOptions
{
    public Dictionary<string, string[]> Permissions { get; init; } = [];
    public Dictionary<string, string[]> Roles { get; init; } = [];
}