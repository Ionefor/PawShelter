using PawShelter.Application.Dto;

namespace PawShelter.Application.Volunteers.Create;

public record CreateVolunteerRequest(
             FullNameDto FullNameDto,
             string Description,
             string Email,
             string PhoneNumber,
             int Experience,
             IEnumerable<RequisiteDto>? Requisites,
             IEnumerable<SocialNetworkDto>? SocialNetworks)
{
    public CreateVolunteerCommand ToCommand() =>
            new(FullNameDto,
                Description,
                Email,
                PhoneNumber,
                Experience,
                Requisites,
                SocialNetworks);
}
