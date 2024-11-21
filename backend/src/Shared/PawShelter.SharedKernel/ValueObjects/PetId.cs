namespace PawShelter.SharedKernel.ValueObjects;

public class PetId : BaseId<PetId>
{
    private PetId(PetId id) : base(id) {}
    
    public static implicit operator Guid(PetId petId) => petId.Id;
    public static implicit operator PetId(Guid id) => new(id);
    
}