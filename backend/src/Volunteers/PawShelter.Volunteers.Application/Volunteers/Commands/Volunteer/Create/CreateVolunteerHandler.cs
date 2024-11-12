using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PawShelter.Core.Abstractions;
using PawShelter.Core.Extensions;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.ValueObjects;
using PawShelter.Volunteers.Domain.ValueObjects.ForVolunteer;
using PawShelter.Volunteers.Domain.ValueObjects.Shared;

namespace PawShelter.Volunteers.Application.Volunteers.Commands.Volunteer.Create;

public class CreateVolunteerHandler : ICommandHandler<Guid, CreateVolunteerCommand>
{
    private readonly ILogger<CreateVolunteerHandler> _logger;
    private readonly IValidator<CreateVolunteerCommand> _validator;
    private readonly IVolunteerRepository _volunteerRepository;

    public CreateVolunteerHandler(
        IVolunteerRepository volunteerRepository,
        IValidator<CreateVolunteerCommand> validator,
        ILogger<CreateVolunteerHandler> logger)
    {
        _volunteerRepository = volunteerRepository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        CreateVolunteerCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var volunteerId = VolunteerId.NewVolonteerId();

        var fullName = FullName.Create(
            command.FullNameDto.FirstName,
            command.FullNameDto.MiddleName,
            command.FullNameDto.LastName).Value;

        var email = Email.Create(command.Email).Value;

        var description = Description.Create(command.Description).Value;

        var number = PhoneNumber.Create(command.PhoneNumber).Value;

        var experience = Experience.Create(command.Experience).Value;

        var requisiteList = command.Requisites.Select(r =>
            Requisite.Create(r.Name, r.Description).Value);
        
        var requisites = new Requisites(requisiteList);

        var socialNetworks = new SocialNetworks(command.SocialNetworks.Select(s =>
            SocialNetwork.Create(s.Name, s.Link).Value).ToList());

        var volunteer = new Domain.Aggregate.Volunteer(
            volunteerId, fullName, email, description,
            number, experience, requisites, socialNetworks);
        
        await _volunteerRepository.Add(volunteer, cancellationToken);

        _logger.LogInformation(
            "Volunteer {firstName} {middleName} created with id: {volunteerId}",
            fullName.FirstName, fullName.MiddleName, volunteerId.Value);

        return volunteer.Id.Value;
    }
}