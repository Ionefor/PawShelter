using PawShelter.Application.Dto;

namespace PawShelter.Application.Volunteers.UpdateSocialNetworks;

public record UpdateSocialNetworksCommand(Guid VolunteerId, SocialNetworksDto SocialNetworksDto);