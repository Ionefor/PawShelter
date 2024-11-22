using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PawShelter.Accounts.Application.Abstractions;
using PawShelter.Accounts.Contracts.Responses;
using PawShelter.Accounts.Domain;
using PawShelter.Core.Abstractions;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.Models.Error;

namespace PawShelter.Accounts.Application.Command.Login;

public class LoginHandler : ICommandHandler<LoginResponse, LoginCommand>
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
    public async Task<Result<LoginResponse, ErrorList>> Handle(
        LoginCommand command,
        CancellationToken cancellationToken = default)
    {
        
        var user = await _userManager.Users
              .Include(u => u.Roles)
              .FirstOrDefaultAsync(u => u.Email == command.Email, cancellationToken);
      
        if (user is null)
        {
            return Errors.General.
                NotFound(
                    new ErrorParameters.General.NotFound(
                        nameof(User), nameof(command.Email), command.Email)).ToErrorList();
        }

        var passwordConfirmed = await _userManager.CheckPasswordAsync(user, command.Password);
        if (!passwordConfirmed)
        {
            return Errors.Extra.CredentialsIsInvalid().ToErrorList();
        }
        
        var accessToken = _tokenProvider.GenerateAccessToken(user, cancellationToken);
        var refreshToken = _tokenProvider.GenerateRefreshToken(user, accessToken.Result.Jti, cancellationToken);
        
        return new LoginResponse(accessToken.Result.AccessToken, refreshToken.Result);
    }
}