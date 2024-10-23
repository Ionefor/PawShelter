using PawShelter.Application.Dto;
using PawShelter.Application.Volunteers.UpdateSocialNetworks;

namespace PawShelter.API.Controllers.Volunteer.Requests;

public record UpdateSocialNetworksRequest(SocialNetworksDto SocialNetworksDto)
{
    public UpdateSocialNetworksCommand ToCommand(Guid volunteerId) =>
        new(volunteerId, SocialNetworksDto);
}