using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using PawShelter.Core.Abstractions;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.Definitions;
using PawShelter.SharedKernel.Models.Error;
using PawShelter.SharedKernel.ValueObjects;
using PawShelter.SharedKernel.ValueObjects.Ids;

namespace PawShelter.Volunteers.Application.Volunteers.Commands.Pet.SoftDeletePet;

public class SoftDeletePetHandler :
    ICommandHandler<Guid, SoftDeletePetCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReadDbContext _readDbContext;
    private readonly IVolunteerRepository _volunteerRepository;

    public SoftDeletePetHandler(
        [FromKeyedServices(ModulesName.Volunteers)]IUnitOfWork unitOfWork,
        IReadDbContext readDbContext,
        IVolunteerRepository volunteerRepository)
    {
        _unitOfWork = unitOfWork;
        _readDbContext = readDbContext;
        _volunteerRepository = volunteerRepository;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        SoftDeletePetCommand command,
        CancellationToken cancellationToken = default)
    {
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
        
        var volunteerResult = await _volunteerRepository.
                GetById(VolunteerId.Create(command.VolunteerId), cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var petId = PetId.Create(command.PetId);
        
        var petForDelete = await _volunteerRepository.
                GetPetById(petId, cancellationToken);
        
        if(petForDelete.IsFailure)
            return petForDelete.Error.ToErrorList();
        
        volunteerResult.Value.SoftDeletePet(petForDelete.Value);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return petForDelete.Value.Id.Id;
    }
}