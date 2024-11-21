using PawShelter.Core.Abstractions;
using PawShelter.Core.Dto;
using PawShelter.Volunteers.Contracts.Dto.Command;

namespace PawShelter.Volunteers.Application.Volunteers.Commands.Volunteer.UpdateSocialNetworks;

public record UpdateSocialNetworksCommand(Guid VolunteerId, SocialNetworksDto SocialNetworksDto) : ICommand;