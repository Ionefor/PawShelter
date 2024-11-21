using PawShelter.SharedKernel.Models.Abstractions;

namespace PawShelter.SharedKernel.ValueObjects.Ids;

public class SpeciesId : BaseId<SpeciesId>
{
    private SpeciesId(SpeciesId id) : base(id) {}
    public static implicit operator Guid(SpeciesId speciesId) => speciesId.Id;
    public static implicit operator SpeciesId(Guid id) => new(id);
}
