namespace PawShelter.Core.Dto.Queries;

public record SortingParamsDto(
    bool? VolunteerId,
    bool? Name,
    bool? Color,
    bool? Species,
    bool? Breed,
    bool? PetCharacteristics,
    bool? BirthDate,
    bool? Address);
