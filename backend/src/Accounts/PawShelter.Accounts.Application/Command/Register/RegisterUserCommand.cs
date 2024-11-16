using PawShelter.Core.Abstractions;

namespace PawShelter.Accounts.Application.Command.Register;

public record RegisterUserCommand(
    string Email, string UserName, string Password) : ICommand;