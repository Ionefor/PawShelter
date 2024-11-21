using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PawShelter.Core.Abstractions;
using PawShelter.Core.Extensions;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.ValueObjects;

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
        [FromKeyedServices("Volunteers")]IUnitOfWork unitOfWork)
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
            return Errors.General.ValueIsInvalid(
                "pet status").ToErrorList();
        }
        
        var volunteerExist = _readDbContext.Volunteers.
            Any(v => v.Id == command.VolunteerId);
        if (!volunteerExist)
        {
            return Error.NotFound(
                "volunteer.not.found", "Volunteer not found").ToErrorList();
        }
            
        var petExist = _readDbContext.Pets.
            Any(
                p => p.Id == command.PetId &&
                     p.VolunteerId == command.VolunteerId);
        
        if (!petExist)
        {
            return Error.NotFound(
                "pet.not.found", "pet not found").ToErrorList();
        }
        
        var petId = PetId.Create(command.PetId);
        var petResult = await _volunteerRepository.GetPetById(
            petId, cancellationToken);
        
        if (petResult.IsFailure)
            return petResult.Error.ToErrorList();
        
        petResult.Value.UpdatePetStatus(statusEnum);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return petResult.Value.Id.Id;
    }
}