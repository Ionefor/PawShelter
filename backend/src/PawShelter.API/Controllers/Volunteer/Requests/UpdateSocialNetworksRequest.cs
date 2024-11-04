using PawShelter.Application.Dto;
using PawShelter.Application.Volunteers.UseCases.UpdateSocialNetworks;

namespace PawShelter.API.Controllers.Volunteer.Requests;

public record UpdateSocialNetworksRequest(SocialNetworksDto SocialNetworksDto)
{
    public UpdateSocialNetworksCommand ToCommand(Guid volunteerId)
    {
        return new UpdateSocialNetworksCommand(volunteerId, SocialNetworksDto);
    }
}