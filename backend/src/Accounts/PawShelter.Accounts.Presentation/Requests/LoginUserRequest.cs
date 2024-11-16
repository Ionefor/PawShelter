using PawShelter.Accounts.Application.Command.Login;

namespace PawShelter.Accounts.Presentation.Requests;

public record LoginUserRequest(string Email, string Password)
{
    public LoginCommand ToCommand() =>
        new(Email, Password);
}