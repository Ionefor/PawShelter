using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PawShelter.Core.Abstractions;
using PawShelter.Core.Extensions;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.Definitions;
using PawShelter.SharedKernel.Models.Error;
using PawShelter.SharedKernel.ValueObjects;
using PawShelter.SharedKernel.ValueObjects.Ids;

namespace PawShelter.Volunteers.Application.Volunteers.Commands.Pet.UpdatePetStatus;

public class UpdatePetStatusHandler :
    ICommandHandler<Guid, UpdatePetStatusCommand>
{
    private readonly IValidator<UpdatePetStatusCommand> _validator;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IReadDbContext _readDbContext;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePetStatusHandler(
        IValidator<UpdatePetStatusCommand> validator,
        IVolunteerRepository volunteerRepository,
        IReadDbContext readDbContext,
        [FromKeyedServices(ModulesName.Volunteers)]IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _volunteerRepository = volunteerRepository;
        _readDbContext = readDbContext;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid, ErrorList>> Handle(
        UpdatePetStatusCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();
        
        Enum.TryParse(typeof(PetStatus), command.Status, out var status);
        var statusEnum = (PetStatus)status!;
        
        if (statusEnum == PetStatus.FoundHome)
        {
            return Errors.General.
                ValueIsInvalid(new ErrorParameters.General.ValueIsInvalid(nameof(PetStatus))).ToErrorList();
        }
        
        var volunteerExist = _readDbContext.Volunteers.
            Any(v => v.Id == command.VolunteerId);
        
        if (!volunteerExist)
        {
            return Errors.General.NotFound(
                new ErrorParameters.General.NotFound(
                    nameof(Volunteer), nameof(VolunteerId), command.VolunteerId)).ToErrorList();
        }
            
        var petExist = _readDbContext.Pets.
            Any(
                p => p.Id == command.PetId &&
                     p.VolunteerId == command.VolunteerId);
        
        if (!petExist)
        {
            return Errors.General.NotFound(
                new ErrorParameters.General.NotFound(
                    nameof(Pet), nameof(PetId), command.PetId)).ToErrorList();
        }
        
        var petId = PetId.Create(command.PetId);
        var petResult = await _volunteerRepository.
            GetPetById(petId, cancellationToken);
        
        if (petResult.IsFailure)
            return petResult.Error.ToErrorList();
        
        var volunteerId = VolunteerId.Create(command.VolunteerId);
        var volunteerResult = await _volunteerRepository.
            GetById(volunteerId, cancellationToken);
        
        volunteerResult.Value.UpdatePetStatus(petResult.Value, statusEnum);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return petResult.Value.Id.Id;
    }
}