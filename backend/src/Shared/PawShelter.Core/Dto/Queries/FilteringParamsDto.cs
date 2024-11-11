
namespace PawShelter.Core.Dto.Queries;

public record FilteringParamsDto(
    Guid? VolunteerId,
    string? Name,
    string? Color,
    Guid? SpeciesId,
    Guid? BreedId,
    PetCharacteristicsDto? PetCharacteristics,
    DateOnly? BirthDate,
    AddressDto? Address);