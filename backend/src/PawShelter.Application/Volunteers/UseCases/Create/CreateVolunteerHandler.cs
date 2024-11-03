using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PawShelter.Application.Abstractions;
using PawShelter.Application.Database;
using PawShelter.Application.Extensions;
using PawShelter.Domain.PetsManagement.Aggregate;
using PawShelter.Domain.PetsManagement.ValueObjects.ForVolunteer;
using PawShelter.Domain.PetsManagement.ValueObjects.Ids;
using PawShelter.Domain.PetsManagement.ValueObjects.Shared;
using PawShelter.Domain.Shared;

namespace PawShelter.Application.Volunteers.UseCases.Create;

public class CreateVolunteerHandler : ICommandHandler<Guid, CreateVolunteerCommand>
{
    private readonly ILogger<CreateVolunteerHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateVolunteerCommand> _validator;
    private readonly IVolunteerRepository _volunteerRepository;

    public CreateVolunteerHandler(
        IVolunteerRepository volunteerRepository,
        IValidator<CreateVolunteerCommand> validator,
        ILogger<CreateVolunteerHandler> logger,
        IUnitOfWork unitOfWork)
    {
        _volunteerRepository = volunteerRepository;
        _validator = validator;
        _logger = logger;
        _unitOfWork = unitOfWork;
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

        var volunteer = new Volunteer(
            volunteerId, fullName, email, description,
            number, experience, requisites, socialNetworks);

        await _volunteerRepository.Add(volunteer, cancellationToken);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation(
            "Volunteer {firstName} {middleName} created with id: {volunteerId}",
            fullName.FirstName, fullName.MiddleName, volunteerId.Value);

        return volunteer.Id.Value;
    }
}