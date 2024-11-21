using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PawShelter.Core.Abstractions;
using PawShelter.Core.Extensions;
using PawShelter.Core.Models;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.Definitions;
using PawShelter.SharedKernel.Models.Error;
using PawShelter.SharedKernel.ValueObjects;
using PawShelter.SharedKernel.ValueObjects.Ids;
using PawShelter.Species.Contracts;
using PawShelter.Volunteers.Domain.ValueObjects;

namespace PawShelter.Volunteers.Application.Volunteers.Commands.Pet.UpdatePet;

public class UpdatePetHandler :
    ICommandHandler<Guid, UpdatePetCommand>
{
    private readonly IValidator<UpdatePetCommand> _validator;
    private readonly IReadDbContext _readDbContext;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdatePetHandler> _logger;
    private readonly ISpeciesContract _contract;

    public UpdatePetHandler(
        IValidator<UpdatePetCommand> validator,
        IReadDbContext readDbContext,
        IVolunteerRepository volunteerRepository,
        [FromKeyedServices(ModulesName.Volunteers)]IUnitOfWork unitOfWork,
        ILogger<UpdatePetHandler> logger,
        ISpeciesContract contract)
    {
        _validator = validator;
        _readDbContext = readDbContext;
        _volunteerRepository = volunteerRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _contract = contract;
    }
    public async Task<Result<Guid, ErrorList>> Handle(
        UpdatePetCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();
        
        var species = await _contract.
            SpeciesExists(command.Species, cancellationToken);
        
        if (species is null)
        {
            return Errors.General.NotFound(
                new ErrorParameters.General.NotFound(
                    nameof(command.Species), nameof(command.Species), command.Species)).ToErrorList();
        }
        
        var breed = await _contract.
            BreedExists(command.Breed, cancellationToken);
        
        if (breed is null)
        {
            return Errors.General.NotFound(
                new ErrorParameters.General.NotFound(
                    nameof(command.Breed), nameof(command.Breed), command.Breed)).ToErrorList();
        }

        var petExist = await _readDbContext.Pets.
            FirstOrDefaultAsync(s => s.Id == command.PetId, cancellationToken);
        
        if (petExist is null)
        {
            return Errors.General.NotFound(
                new ErrorParameters.General.NotFound(
                    nameof(Pet), nameof(PetId), command.PetId)).ToErrorList();
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
        
        var speciesId = SpeciesId.Create(species.SpeciesId);
        var breedId = BreedId.Create(breed.BreedId);
        var speciesBreedId = new SpeciesBreedsId(speciesId, breedId.Id);
        
        volunteerResult.Value.UpdatePet(petToUpdate!, petName, petDescription, speciesBreedId, color,
            health, address, number, petCharacteristics, command.IsCastrated,
            command.IsVaccinated, birthday, command.PublicationDate, requisites);
        
       await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation(
            "Pet {petName} has been updated  with id: {petId}",
            petName, petId);
        
        return petToUpdate!.Id.Id;
    }
}