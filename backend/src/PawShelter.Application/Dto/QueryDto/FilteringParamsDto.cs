using PawShelter.Domain.PetsManagement.ValueObjects.ForPet;

namespace PawShelter.Application.Dto.QueryDto;

public record FilteringParamsDto(
    Guid? VolunteerId,
    string? Name,
    string? Color,
    Guid? SpeciesId,
    Guid? BreedId,
    PetCharacteristicsDto? PetCharacteristics,
    DateOnly? BirthDate,
    AddressDto? Address);