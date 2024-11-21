using PawShelter.Core.Dto;
using PawShelter.Volunteers.Contracts.Dto.Command;

namespace PawShelter.Volunteers.Contracts.Dto.Models;

public class VolunteerDto
{
    public Guid Id { get; init; }

    public FullNameDto FullName { get; init; }

    public string Email { get; init; }
    
    public string Description { get; init; } = null!;

    public string PhoneNumber { get; init; } = null!;

    public int Experience { get; init; }

    public IReadOnlyList<SocialNetworkDto> SocialNetworks { get; init; } = [];

    public IReadOnlyList<RequisiteDto> Requisites { get; init; } = [];

    public bool IsDeleted { get; init; }

    public PetDto[] Pets { get; init; } = [];
}