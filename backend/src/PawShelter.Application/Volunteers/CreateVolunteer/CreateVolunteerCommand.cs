using PawShelter.Application.Dto;

namespace PawShelter.Application.Volunteers.CreateVolunteer
{
    public record CreateVolunteerCommand(
                FullNameDto fullNameDto,
                string description,
                string email,
                string phoneNumber,
                int experience,
                IEnumerable<RequisiteDto>? requisites,
                IEnumerable<SocialNetworkDto>? socialNetworks);
}
