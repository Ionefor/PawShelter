using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;
using FluentValidation;
using PawShelter.Application.Abstractions;
using PawShelter.Application.Database;
using PawShelter.Application.Extensions;
using PawShelter.Domain.PetsManagement.ValueObjects.ForPet;
using PawShelter.Domain.PetsManagement.ValueObjects.Ids;
using PawShelter.Domain.Shared;

namespace PawShelter.Application.Volunteers.UseCases.SetMainPetPhoto;

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
        IUnitOfWork unitOfWork)
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

        var filePath = FilePath.ToFilePath(command.FilePath);
        var mainPhoto = new PetPhoto(filePath, true);
        petResult.Value.SetMainPhoto(mainPhoto);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return petResult.Value.Id.Value;
    }
}