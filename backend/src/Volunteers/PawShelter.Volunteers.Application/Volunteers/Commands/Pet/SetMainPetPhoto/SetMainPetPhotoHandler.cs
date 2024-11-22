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
using PawShelter.Volunteers.Domain.ValueObjects;

namespace PawShelter.Volunteers.Application.Volunteers.Commands.Pet.SetMainPetPhoto;

public class SetMainPetPhotoHandler :
    ICommandHandler<Guid, SetMainPetPhotoCommand>
{
    private readonly IValidator<SetMainPetPhotoCommand> _validator;
    private readonly IReadDbContext _readDbContext;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SetMainPetPhotoHandler(
        IValidator<SetMainPetPhotoCommand> validator,
        IReadDbContext readDbContext,
        IVolunteerRepository volunteerRepository,
        [FromKeyedServices(ModulesName.Volunteers)]IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _readDbContext = readDbContext;
        _volunteerRepository = volunteerRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid, ErrorList>> Handle(
        SetMainPetPhotoCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();
        
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
        var petResult = await _volunteerRepository.GetPetById(
            petId, cancellationToken);
        
        if (petResult.IsFailure)
            return petResult.Error.ToErrorList();
        
        var volunteerId = VolunteerId.Create(command.VolunteerId);
        var volunteerResult = await _volunteerRepository.
            GetById(volunteerId, cancellationToken);
        
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var filePath = FilePath.ToFilePath(command.FilePath);
        var mainPhoto = new PetPhoto(filePath, true);
        
        volunteerResult.Value.SetMainPetPhoto(petResult.Value, mainPhoto);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return petResult.Value.Id.Id;
    }
}