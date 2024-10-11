using PawShelter.Application.Dto;

namespace PawShelter.Application.Volunteers.UpdateSocialNetworks;

public record UpdateSocialNetworksRequest(SocialNetworksDto SocialNetworksDto)
{
    public UpdateSocialNetworksCommand ToCommand(Guid volunteerId) =>
        new(volunteerId, SocialNetworksDto);
}