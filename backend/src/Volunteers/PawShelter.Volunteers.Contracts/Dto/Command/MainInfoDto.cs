using PawShelter.Core.Dto;

namespace PawShelter.Volunteers.Contracts.Dto.Command;

public record MainInfoDto(
    FullNameDto FullName,
    string Email,
    string Description,
    string PhoneNumber,
    int Experience);