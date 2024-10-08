using PawShelter.Application.Dto;
namespace PawShelter.Application.Volunteers.CreateVolunteer;

public record CreateVolunteerRequest(
             FullNameDto fullNameDto,
             string description,
             string email,
             string phoneNumber,
             int experience,
             IEnumerable<RequisiteDto>? requisites,
             IEnumerable<SocialNetworkDto>? socialNetworks)
{
    public CreateVolunteerCommand ToCommand() =>
            new(fullNameDto,
                description,
                email,
                phoneNumber,
                experience,
                requisites,
                socialNetworks);
}
