using PawShelter.Core.Abstractions;
using PawShelter.Core.Dto;
using PawShelter.Volunteers.Contracts.Dto.Command;

namespace PawShelter.Volunteers.Application.Volunteers.Commands.Volunteer.Create;

public record CreateVolunteerCommand(
    FullNameDto FullNameDto,
    string Description,
    string Email,
    string PhoneNumber,
    int Experience,
    IEnumerable<RequisiteDto>? Requisites,
    IEnumerable<SocialNetworkDto>? SocialNetworks) : ICommand;