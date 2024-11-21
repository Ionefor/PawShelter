using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PawShelter.Core.Abstractions;
using PawShelter.Core.Extensions;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.ValueObjects;
using PawShelter.Volunteers.Domain.ValueObjects.ForVolunteer;
using PawShelter.Volunteers.Domain.ValueObjects.Shared;

namespace PawShelter.Volunteers.Application.Volunteers.Commands.Volunteer.UpdateMainInfo;

public class UpdateMainInfoHandler : ICommandHandler<Guid, UpdateMainInfoCommand>
{
    private readonly ILogger<UpdateMainInfoCommand> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateMainInfoCommand> _validator;
    private readonly IVolunteerRepository _volunteerRepository;

    public UpdateMainInfoHandler(
        IVolunteerRepository volunteerRepository,
        IValidator<UpdateMainInfoCommand> validator,
        ILogger<UpdateMainInfoCommand> logger,
        [FromKeyedServices("Volunteers")]IUnitOfWork unitOfWork)
    {
        _volunteerRepository = volunteerRepository;
        _validator = validator;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        UpdateMainInfoCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var volunteerResult =
            await _volunteerRepository.GetById(VolunteerId.Create(command.VolunteerId), cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var fullName = FullName.Create(
            command.MainInfoDto.FullName.FirstName,
            command.MainInfoDto.FullName.MiddleName,
            command.MainInfoDto.FullName.LastName).Value;

        var description = Description.Create(command.MainInfoDto.Description).Value;
        var email = Email.Create(command.MainInfoDto.Email).Value;
        var phoneNumber = PhoneNumber.Create(command.MainInfoDto.PhoneNumber).Value;
        var experience = Experience.Create(command.MainInfoDto.Experience).Value;

        volunteerResult.Value.UpdateMainInfo(
            fullName, email, description, phoneNumber, experience);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Main info of the Volunteer {firstName} {middleName} has been updated",
            fullName.FirstName, fullName.MiddleName);

        return volunteerResult.Value.Id.Id;
    }
}