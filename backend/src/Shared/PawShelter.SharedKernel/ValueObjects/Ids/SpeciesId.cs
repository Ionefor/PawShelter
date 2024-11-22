using PawShelter.SharedKernel.Models.Abstractions;

namespace PawShelter.SharedKernel.ValueObjects.Ids;

public class SpeciesId(Guid id) : BaseId<SpeciesId>(id)
{
    public static implicit operator Guid(SpeciesId speciesId) => speciesId.Id;
    public static implicit operator SpeciesId(Guid id) => new(id);
}
