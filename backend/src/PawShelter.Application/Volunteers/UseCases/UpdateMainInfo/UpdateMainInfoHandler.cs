﻿using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PawShelter.Application.Abstractions;
using PawShelter.Application.Database;
using PawShelter.Application.Extensions;
using PawShelter.Domain.PetsManagement.ValueObjects.ForVolunteer;
using PawShelter.Domain.PetsManagement.ValueObjects.Ids;
using PawShelter.Domain.PetsManagement.ValueObjects.Shared;
using PawShelter.Domain.Shared;

namespace PawShelter.Application.Volunteers.UseCases.UpdateMainInfo;

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
        IUnitOfWork unitOfWork)
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

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation(
            "Main info of the Volunteer {firstName} {middleName} has been updated",
            fullName.FirstName, fullName.MiddleName);

        return volunteerResult.Value.Id.Value;
    }
}