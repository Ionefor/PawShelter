namespace PawShelter.SharedKernel.ValueObjects;

public class SpeciesId : BaseId<SpeciesId>
{
    private SpeciesId(PetId id) : base(id) {}
    public static implicit operator Guid(SpeciesId speciesId) => speciesId.Id;
    public static implicit operator SpeciesId(Guid id) => new(id);
}
