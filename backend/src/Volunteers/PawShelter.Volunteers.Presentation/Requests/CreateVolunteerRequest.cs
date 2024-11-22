using PawShelter.Core.Dto;
using PawShelter.Volunteers.Application.Volunteers.Commands.Volunteer.Create;
using PawShelter.Volunteers.Contracts.Dto.Command;

namespace PawShelter.Volunteers.Presentation.Requests;

public record CreateVolunteerRequest(
    FullNameDto FullNameDto,
    string Description,
    string Email,
    string PhoneNumber,
    int Experience,
    IEnumerable<RequisiteDto>? Requisites,
    IEnumerable<SocialNetworkDto>? SocialNetworks)
{
    public CreateVolunteerCommand ToCommand()
    {
        return new CreateVolunteerCommand(FullNameDto,
            Description,
            Email,
            PhoneNumber,
            Experience,
            Requisites,
            SocialNetworks);
    }
}
