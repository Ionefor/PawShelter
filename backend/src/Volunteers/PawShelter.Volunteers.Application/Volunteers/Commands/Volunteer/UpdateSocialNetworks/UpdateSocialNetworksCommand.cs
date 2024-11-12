using PawShelter.Core.Abstractions;
using PawShelter.Core.Dto;

namespace PawShelter.Volunteers.Application.Volunteers.Commands.Volunteer.UpdateSocialNetworks;

public record UpdateSocialNetworksCommand(Guid VolunteerId, SocialNetworksDto SocialNetworksDto) : ICommand;