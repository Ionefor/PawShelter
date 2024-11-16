using PawShelter.Core.Abstractions;

namespace PawShelter.Accounts.Application.Command.Login;

public record LoginCommand(string Email, string Password) : ICommand;
