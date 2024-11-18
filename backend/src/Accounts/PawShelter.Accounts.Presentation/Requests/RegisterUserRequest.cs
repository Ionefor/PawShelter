using PawShelter.Accounts.Application.Command.Register;
using PawShelter.Core.Dto;
using PawShelter.SharedKernel.ValueObjects;

namespace PawShelter.Accounts.Presentation.Requests;

public record RegisterUserRequest(string Email, string UserName, string Password, FullNameDto FullName)
{
    public RegisterUserCommand ToCommand() =>
        new(Email, UserName, Password, FullName);
}