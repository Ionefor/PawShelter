using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PawShelter.Application.Abstractions;
using PawShelter.Application.Database;
using PawShelter.Application.Extensions;
using PawShelter.Application.Species;
using PawShelter.Domain.PetsManagement;
using PawShelter.Domain.PetsManagement.Entities;
using PawShelter.Domain.PetsManagement.ValueObjects.ForPet;
using PawShelter.Domain.PetsManagement.ValueObjects.Ids;
using PawShelter.Domain.PetsManagement.ValueObjects.Shared;
using PawShelter.Domain.Shared;
using PawShelter.Domain.SpeciesManagement.ValueObjects.Ids;

namespace PawShelter.Application.Volunteers.UseCases.AddPet;

public class AddPetHandler : ICommandHandler<Guid, AddPetCommand>
{
    private readonly ILogger<AddPetHandler> _logger;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReadDbContext _readDbContext;
    private readonly IValidator<AddPetCommand> _validator;
    private readonly IVolunteerRepository _volunteerRepository;

    public AddPetHandler(
        ILogger<AddPetHandler> logger,
        IValidator<AddPetCommand> validator,
        ISpeciesRepository speciesRepository,
        IVolunteerRepository volunteerRepository,
        IUnitOfWork unitOfWork,
        IReadDbContext readDbContext)
    {
        _logger = logger;
        _validator = validator;
        _speciesRepository = speciesRepository;
        _volunteerRepository = volunteerRepository;
        _unitOfWork = unitOfWork;
        _readDbContext = readDbContext;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        AddPetCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        // var existSpeciesResult = await _speciesRepository.ExistSpecies(command.Species, cancellationToken);
        // if (existSpeciesResult.IsFailure)
        //     return existSpeciesResult.Error.ToErrorList();
        //
        // var existBreedResult = await _speciesRepository.ExistBreed(command.Breed, cancellationToken);
        // if (existBreedResult.IsFailure)
        //     return existBreedResult.Error.ToErrorList();
        
        var species = await _readDbContext.Species.
            FirstOrDefaultAsync(
                s => s.Species == command.Species, cancellationToken);
        if (species is null)
        {
            return Error.NotFound(
                "species.not.found", "species not found").ToErrorList();
        }
        
        var breed = await _readDbContext.Breeds.
            FirstOrDefaultAsync(
                s => s.Breed == command.Breed, cancellationToken);
        if (breed is null)
        {
            return Error.NotFound(
                "breed.not.found", "breed not found").ToErrorList();
        }
        
        var speciesId = SpeciesId.Create(species.SpeciesId);
        var breedId = BreedId.Create(breed.BreedId);
        var speciesBreedId = new SpeciesBreedsId(speciesId, breedId.Value);
        
        var volunteerId = VolunteerId.Create(command.VolunteerId);

        var volunteerResult = await _volunteerRepository.GetById(volunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var petId = PetId.NewPetId();

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
            command.PetCharacteristicsDto.Width).Value;

        var birthday = Birthday.Create(command.Birthday).Value;

        var requisiteList = command.RequisitesDto.Requisites.Select(r => Requisite.Create(r.Name, r.Description).Value);

        var requisites = new Requisites(requisiteList);
        
        Enum.TryParse(typeof(PetStatus), command.Status, out var status);
        var statusEnum = (PetStatus)status!;
        
        var pet = new Pet(
            petId, petName, petDescription, speciesBreedId, color,
            health, address, number, petCharacteristics, command.IsCastrated,
            command.IsVaccinated, birthday, command.PublicationDate,
            requisites, statusEnum);

        volunteerResult.Value.AddPet(pet);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Pet {petName} has been added to the volunteer with id: {volunteerId}",
            petName, volunteerId);

        return pet.Id.Value;
    }
}