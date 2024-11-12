using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PawShelter.Core.Abstractions;
using PawShelter.Core.Extensions;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.ValueObjects;
using PawShelter.Species.Contracts;
using PawShelter.Volunteers.Domain.ValueObjects.ForPet;
using PawShelter.Volunteers.Domain.ValueObjects.Shared;

namespace PawShelter.Volunteers.Application.Volunteers.Commands.Pet.AddPet;

public class AddPetHandler : ICommandHandler<Guid, AddPetCommand>
{
    private readonly ILogger<AddPetHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReadDbContext _readDbContext;
    private readonly ISpeciesContract _contract;
    private readonly IValidator<AddPetCommand> _validator;
    private readonly IVolunteerRepository _volunteerRepository;

    public AddPetHandler(
        ILogger<AddPetHandler> logger,
        IValidator<AddPetCommand> validator,
        IVolunteerRepository volunteerRepository,
        [FromKeyedServices("Volunteers")]IUnitOfWork unitOfWork,
        IReadDbContext readDbContext,
        ISpeciesContract contract)
    {
        _logger = logger;
        _validator = validator;
        _volunteerRepository = volunteerRepository;
        _unitOfWork = unitOfWork;
        _readDbContext = readDbContext;
        _contract = contract;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        AddPetCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var species = await _contract.
            SpeciesExists(command.Species, cancellationToken);
        
        if (species is null)
        {
            return Error.NotFound(
                "species.not.found", "species not found").ToErrorList();
        }
        
        var breed = await _contract.
            BreedExists(command.Breed, cancellationToken);
        
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
            command.PetCharacteristicsDto.Weight).Value;
        
        var birthday = Birthday.Create(command.Birthday).Value;
        
        var requisiteList = command.RequisitesDto.Requisites.Select(r => Requisite.Create(r.Name, r.Description).Value);
        
        var requisites = new Requisites(requisiteList);
        
        Enum.TryParse(typeof(PetStatus), command.Status, out var status);
        var statusEnum = (PetStatus)status!;
        
        var pet = new Domain.Entities.Pet(
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