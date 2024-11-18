using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PawShelter.Accounts.Application.Abstractions;
using PawShelter.Accounts.Domain;
using PawShelter.Accounts.Domain.Accounts;
using PawShelter.Core.Abstractions;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.ValueObjects;

namespace PawShelter.Accounts.Application.Command.Register;

public class RegisterUserHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly IAccountManager _accountManager;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly ILogger<RegisterUserHandler> _logger;

    public RegisterUserHandler(
        IAccountManager accountManager,
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        ILogger<RegisterUserHandler> logger)
    {
        _accountManager = accountManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }
    public async Task<UnitResult<ErrorList>> Handle(
        RegisterUserCommand command,
        CancellationToken cancellationToken = default)
    {
        var fullName = FullName.Create(
            command.FullName.FirstName, command.FullName.MiddleName, command.FullName.LastName).Value;

        var participantRole = await _roleManager.FindByNameAsync(ParticipantAccount.Participant);
        if (participantRole is null)
        {
            return Error.NotFound(
                "role.not.found", "Participant is doesn't exist").ToErrorList();
        }
        
        var user = User.CreateParticipant(fullName, command.UserName, command.Email, participantRole!);
        if (user.IsFailure)
            return user.Error.ToErrorList();
        
        var result = await _userManager.CreateAsync(user.Value, command.Password);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => Error.Failure(e.Code, e.Description)).ToList();
              
            return new ErrorList(errors);
        }
        
        var participantAccount = ParticipantAccount.Create(user.Value);

        await _accountManager.CreateParticipantAccount(participantAccount);

        _logger.LogInformation("Created user with username {Name}", command.UserName);

        return UnitResult.Success<ErrorList>();
    }
}