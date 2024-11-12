using PawShelter.Core.Abstractions;
using PawShelter.Core.Dto;

namespace PawShelter.Volunteers.Application.Volunteers.Commands.Pet.UpdatePet;

public record UpdatePetCommand(
    Guid VolunteerId,
    Guid PetId,
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
    string Species,
    string Breed,
    RequisitesDto RequisitesDto) : ICommand;