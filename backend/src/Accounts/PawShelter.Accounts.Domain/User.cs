using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using PawShelter.Accounts.Domain.Accounts;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.ValueObjects;

namespace PawShelter.Accounts.Domain;

public class User : IdentityUser<Guid>
{
    public List<SocialNetwork> SocialNetworks { get; set; } = [];
    
    public List<Role> Roles { get; set; } = [];

    public string? Photo { get; set; } = null;
    
    public FullName FullName { get; set; } = null!;

    public static Result<User, Error> CreateAdmin(string userName, string email, Role role)
    {
        if (role.Name != AdminAccount.Admin)
            return Error.Failure("incorrect.role", "Role must be an Admin");
        
        return new User
        {
            FullName = FullName.Create(userName, userName, userName).Value,
            UserName = userName,
            Email = email,
            Roles = [role]
        };
    }
    
    public static Result<User, Error> CreateParticipant(
        FullName fullName, string userName, string email, Role role)
    {
        if (role.Name != ParticipantAccount.Participant)
            return Error.Failure("incorrect.role", "Role must be an Participant");
        
        return new User
        {
            FullName = fullName,
            UserName = userName,
            Email = email,
            Roles = [role]
        };
    }
}