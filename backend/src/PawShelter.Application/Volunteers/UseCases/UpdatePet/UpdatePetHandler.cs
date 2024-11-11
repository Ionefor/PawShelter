using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PawShelter.Application.Abstractions;
using PawShelter.Application.Database;
using PawShelter.Application.Extensions;
using PawShelter.Domain.PetsManagement.ValueObjects.ForPet;
using PawShelter.Domain.PetsManagement.ValueObjects.Ids;
using PawShelter.Domain.PetsManagement.ValueObjects.Shared;
using PawShelter.Domain.Shared;
using PawShelter.Domain.SpeciesManagement.ValueObjects.Ids;

namespace PawShelter.Application.Volunteers.UseCases.UpdatePet;

public class UpdatePetHandler :
    ICommandHandler<Guid, UpdatePetCommand>
{
    private readonly IValidator<UpdatePetCommand> _validator;
    private readonly IReadDbContext _readDbContext;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdatePetHandler> _logger;

    public UpdatePetHandler(
        IValidator<UpdatePetCommand> validator,
        IReadDbContext readDbContext,
        IVolunteerRepository volunteerRepository,
        IUnitOfWork unitOfWork,
        ILogger<UpdatePetHandler> logger)
    {
        _validator = validator;
        _readDbContext = readDbContext;
        _volunteerRepository = volunteerRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    public async Task<Result<Guid, ErrorList>> Handle(
        UpdatePetCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();
        
        var speciesExist = await _readDbContext.Species.
            FirstOrDefaultAsync(s => s.Species == command.Species, cancellationToken);
        if (speciesExist is null)
        {
            return Error.NotFound(
                "species.not.found", "species not found").ToErrorList();
        }
        
        var breedExist = await _readDbContext.Breeds.
            FirstOrDefaultAsync(s => s.Breed == command.Breed, cancellationToken);
        if (breedExist is null)
        {
            return Error.NotFound(
                "breed.not.found", "breed not found").ToErrorList();
        }

        var petExist = await _readDbContext.Pets.
            FirstOrDefaultAsync(s => s.Id == command.PetId, cancellationToken);
        if (petExist is null)
        {
            return Error.NotFound(
                "pet.not.found", "pet not found").ToErrorList();
        }
        
        var volunteerId = VolunteerId.Create(command.VolunteerId);
        var volunteerResult = await _volunteerRepository.
            GetById(volunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        
        var petId = PetId.Create(command.PetId);
        var petToUpdate = volunteerResult.Value.Pets.
            FirstOrDefault( p => p.Id == petId);
        
        var petName = Name.Create(command.Name).Value;
        var petDescription = Description.Create(command.Description).Value;
        
        var color = Color.Create(command.Color).Value;
        var health = HealthInfo.Create(command.HealthInfo).Value;
        var address = Address.Create(
            command.AddressDto.Country,
            command.AddressDto.City,
            command.AddressDto.Street,
            command.AddressDto.HouseNumber).Value;

        var number = PhoneNumber.Create(command.PhoneNumber).Value;
        var petCharacteristics = PetCharacteristics.Create(
            command.PetCharacteristicsDto.Height,
            command.PetCharacteristicsDto.Weight).Value;

        var birthday = Birthday.Create(command.Birthday).Value;

        var requisiteList = command.RequisitesDto.Requisites.
            Select(r => Requisite.Create(r.Name, r.Description).Value);
        var requisites = new Requisites(requisiteList);
        
        var speciesId = SpeciesId.Create(speciesExist.SpeciesId);
        var breedId = BreedId.Create(breedExist.BreedId);
        var speciesBreedId = new SpeciesBreedsId(speciesId, breedId.Value);
        
        petToUpdate!.UpdatePet(petName, petDescription, speciesBreedId, color,
            health, address, number, petCharacteristics, command.IsCastrated,
            command.IsVaccinated, birthday, command.PublicationDate,
            requisites);
        
        _unitOfWork.SaveChanges();

        _logger.LogInformation(
            "Pet {petName} has been updated  with id: {petId}",
            petName, petId);
        
        return petToUpdate!.Id.Value;
    }
}