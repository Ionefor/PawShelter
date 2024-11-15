using PawShelter.Accounts.Application.Command.Register;

namespace PawShelter.Accounts.Presentation.Requests;

public record RegisterUserRequest(string Email, string UserName, string Password)
{
    public RegisterUserCommand ToCommand() =>
        new(Email, UserName, Password);
}