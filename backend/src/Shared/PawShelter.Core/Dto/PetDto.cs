using PawShelter.SharedKernel;

namespace PawShelter.Core.Dto;

public class PetDto
{
    public Guid Id { get; init; }

    public Guid VolunteerId { get; init; }

    public Guid BreedId { get; init; }

    public Guid SpeciesId { get; init; }

    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;

    public string Color { get; init; }

    public string HealthInfo { get; init; } = null!;

    public AddressDto Address { get; init; } = default!;

    public string PhoneNumber { get; init; } = null!;

    public double Weight { get; init; }

    public double Height { get; init; }

    public bool IsCastrated { get; init; }

    public bool IsVaccinated { get; init; }

    public int Position { get; init; }

    public DateOnly Birthday { get; init; }

    public DateTime PublicationDate { get; init; }

    public IReadOnlyList<PetPhotoDto> Photos { get; init; }

    public IReadOnlyList<RequisiteDto> Requisites { get; init; }

    public PetStatus Status { get; init; }

    public bool IsDeleted { get; init; }
}