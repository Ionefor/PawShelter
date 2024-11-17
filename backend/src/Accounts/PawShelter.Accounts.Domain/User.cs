using Microsoft.AspNetCore.Identity;
using PawShelter.SharedKernel.ValueObjects;

namespace PawShelter.Accounts.Domain;

public class User : IdentityUser<Guid>
{
    public List<SocialNetwork> SocialNetworks { get; set; } = [];
    
    public List<Role> Roles { get; set; } = [];
    
    public string? Photo  { get; set; }
    
    public FullName FullName { get; set; } = null!;
}