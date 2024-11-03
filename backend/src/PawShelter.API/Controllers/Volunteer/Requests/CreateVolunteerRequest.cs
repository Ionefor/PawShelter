using PawShelter.Application.Dto;
using PawShelter.Application.Volunteers.UseCases.Create;

namespace PawShelter.API.Controllers.Volunteer.Requests;

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