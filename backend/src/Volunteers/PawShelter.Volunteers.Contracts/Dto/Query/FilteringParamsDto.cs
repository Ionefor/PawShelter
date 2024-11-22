
using PawShelter.Volunteers.Contracts.Dto.Command;

namespace PawShelter.Volunteers.Contracts.Dto.Query;

public record FilteringParamsDto(
    Guid? VolunteerId,
    string? Name,
    string? Color,
    Guid? SpeciesId,
    Guid? BreedId,
    PetCharacteristicsDto? PetCharacteristics,
    DateOnly? BirthDate,
    AddressDto? Address);