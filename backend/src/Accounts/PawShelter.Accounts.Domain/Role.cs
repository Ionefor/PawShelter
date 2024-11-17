using Microsoft.AspNetCore.Identity;

namespace PawShelter.Accounts.Domain;

public class Role : IdentityRole<Guid>
{
    public List<RolePermission> RolePermission { get; set; } = [];
}