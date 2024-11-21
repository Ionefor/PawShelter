using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using PawShelter.Accounts.Domain.Accounts;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.Models.Error;
using PawShelter.SharedKernel.ValueObjects;

namespace PawShelter.Accounts.Domain;

public class User : IdentityUser<Guid>
{
    public List<SocialNetwork> SocialNetworks { get; init; } = [];
    public List<Role> Roles { get; init; } = [];
    public string? Photo { get; init; }
    public FullName FullName { get; init; } = null!;
    public static Result<User, Error> CreateAdmin(string userName, string email, Role role)
    {
        if (role.Name != AdminAccount.Admin)
        {
            return Errors.Extra.
                RoleIsInvalid(new ErrorParameters.Extra.RoleIsInvalid("Role must be an Admin"));
        }
        
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
        {
            return Errors.Extra.
                RoleIsInvalid(new ErrorParameters.Extra.RoleIsInvalid("Role must be an Participant"));
        }
        
        return new User
        {
            FullName = fullName,
            UserName = userName,
            Email = email,
            Roles = [role]
        };
    }
}