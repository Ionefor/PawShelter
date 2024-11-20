namespace PawShelter.SharedKernel.ValueObjects;

public class BreedId : BaseId<BreedId>
{
    private BreedId(PetId id) : base(id) {}
    
    public static implicit operator Guid(BreedId breedId) => breedId.Id;
    public static implicit operator BreedId(Guid id) => new(id);
}