using PawShelter.Core.Abstractions;
using PawShelter.Core.Dto;
using PawShelter.Volunteers.Contracts.Dto.Command;

namespace PawShelter.Volunteers.Application.Volunteers.Commands.Pet.AddPet;

public record AddPetCommand(
    Guid VolunteerId,
    string Name,
    string Description,
    string Color,
    string HealthInfo,
    string PhoneNumber,
    AddressDto AddressDto,
    PetCharacteristicsDto PetCharacteristicsDto,
    bool IsCastrated,
    bool IsVaccinated,
    DateOnly Birthday,
    DateTime PublicationDate,
    string Status,
    string Species,
    string Breed,
    RequisitesDto RequisitesDto) : ICommand;