namespace PawShelter.Volunteers.Contracts.Dto.Query;

public record SortingParamsDto(
    bool? VolunteerId,
    bool? Name,
    bool? Color,
    bool? Species,
    bool? Breed,
    bool? PetCharacteristics,
    bool? BirthDate,
    bool? Address);
