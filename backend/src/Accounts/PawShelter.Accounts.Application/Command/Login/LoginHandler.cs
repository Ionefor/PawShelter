using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using PawShelter.Accounts.Domain;
using PawShelter.Core.Abstractions;
using PawShelter.SharedKernel;

namespace PawShelter.Accounts.Application.Command.Login;

public class LoginHandler : ICommandHandler<string, LoginCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenProvider _tokenProvider;

    public LoginHandler(
        UserManager<User> userManager,
        ITokenProvider tokenProvider)
    {
        _userManager = userManager;
        _tokenProvider = tokenProvider;
    }
    public async Task<Result<string, ErrorList>> Handle(
        LoginCommand command,
        CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(command.Email);
        if (user is null)
            return Errors.General.NotFound().ToErrorList();
        
        var passwordConfirmed = await _userManager.CheckPasswordAsync(user, command.Password);
        if (!passwordConfirmed)
            return Errors.General.ValueIsInvalid("Your credentials").ToErrorList();

        var token = _tokenProvider.GenerateAccessToken(user, cancellationToken);
        
        return token.Result;
    }
}