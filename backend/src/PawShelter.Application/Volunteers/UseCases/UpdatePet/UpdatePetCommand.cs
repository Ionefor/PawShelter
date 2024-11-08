using PawShelter.Application.Abstractions;
using PawShelter.Application.Dto;

namespace PawShelter.Application.Volunteers.UseCases.UpdatePet;

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