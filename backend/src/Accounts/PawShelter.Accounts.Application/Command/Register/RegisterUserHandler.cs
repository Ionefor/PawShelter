using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using PawShelter.Accounts.Domain;
using PawShelter.Core.Abstractions;
using PawShelter.SharedKernel;

namespace PawShelter.Accounts.Application.Command.Register;

public class RegisterUserHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly UserManager<User> _userManager;

    public RegisterUserHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    public async Task<UnitResult<ErrorList>> Handle(
        RegisterUserCommand command,
        CancellationToken cancellationToken = default)
    {
        var user = new User
        {
            Email = command.Email,
            UserName = command.UserName,
        };
        
        var result = await _userManager.CreateAsync(user, command.Password);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => Error.Failure(e.Code, e.Description)).ToList();
              
            return new ErrorList(errors);
        }
        
        
        return Result.Success<ErrorList>();
    }
}