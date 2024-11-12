using PawShelter.Core.Dto;
using PawShelter.Volunteers.Application.Volunteers.Commands.Volunteer.UpdateSocialNetworks;

namespace PawShelter.Volunteers.Presentation.Requests;

public record UpdateSocialNetworksRequest(SocialNetworksDto SocialNetworksDto)
{
    public UpdateSocialNetworksCommand ToCommand(Guid volunteerId)
    {
        return new UpdateSocialNetworksCommand(volunteerId, SocialNetworksDto);
    }
}