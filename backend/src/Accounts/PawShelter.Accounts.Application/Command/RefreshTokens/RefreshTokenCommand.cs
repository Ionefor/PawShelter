using PawShelter.Core.Abstractions;

namespace PawShelter.Accounts.Application.Command.RefreshTokens;

public record RefreshTokenCommand(string AccessToken, Guid RefreshToken) : ICommand;