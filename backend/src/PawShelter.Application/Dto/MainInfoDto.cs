namespace PawShelter.Application.Dto;

public record MainInfoDto(
    FullNameDto FullName,
    string Email,
    string Description,
    string PhoneNumber,
    int Experience);
