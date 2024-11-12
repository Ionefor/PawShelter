using PawShelter.Core.Dto;
using PawShelter.Volunteers.Application.Volunteers.Commands.Volunteer.Create;

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
