using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PawShelter.Core.Abstractions;
using PawShelter.Core.Extensions;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.ValueObjects;
using PawShelter.Volunteers.Domain.ValueObjects.ForVolunteer;

namespace PawShelter.Volunteers.Application.Volunteers.Commands.Volunteer.UpdateSocialNetworks;

public class UpdateSocialNetworksHandler : ICommandHandler<Guid, UpdateSocialNetworksCommand>
{
    private readonly ILogger<UpdateSocialNetworksCommand> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateSocialNetworksCommand> _validator;
    private readonly IVolunteerRepository _volunteerRepository;

    public UpdateSocialNetworksHandler(
        IVolunteerRepository volunteerRepository,
        IValidator<UpdateSocialNetworksCommand> validator,
        ILogger<UpdateSocialNetworksCommand> logger,
        [FromKeyedServices("Volunteers")]IUnitOfWork unitOfWork)
    {
        _volunteerRepository = volunteerRepository;
        _validator = validator;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        UpdateSocialNetworksCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var volunteerResult =
            await _volunteerRepository.GetById(VolunteerId.Create(command.VolunteerId), cancellationToken);

        var socialNetworks =
            command.SocialNetworksDto.SocialNetworks.Select(s => SocialNetwork.Create(s.Name, s.Link).Value);

        volunteerResult.Value.UpdateSocialNetworks(new SocialNetworks(socialNetworks));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "SocialNetworks of the Volunteer {firstName} {middleName} has been updated",
            volunteerResult.Value.FullName.FirstName,
            volunteerResult.Value.FullName.MiddleName);

        return volunteerResult.Value.Id.Id;
    }
}