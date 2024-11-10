namespace PawShelter.Application.Dto.QueryDto;

public record SortingParamsDto(
    bool? VolunteerId,
    bool? Name,
    bool? Color,
    bool? Species,
    bool? Breed,
    bool? Height,
    bool? Weight,
    bool? BirthDate,
    bool? Address);
