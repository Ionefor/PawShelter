using PawShelter.Application.Abstractions;
using PawShelter.Application.Dto;

namespace PawShelter.Application.Volunteers.UseCases.UpdateSocialNetworks;

public record UpdateSocialNetworksCommand(Guid VolunteerId, SocialNetworksDto SocialNetworksDto) : ICommand;