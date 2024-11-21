using PawShelter.SharedKernel.Models.Abstractions;

namespace PawShelter.SharedKernel.ValueObjects.Ids;

public class PetId : BaseId<PetId>
{
    private PetId(PetId id) : base(id) {}
    public static implicit operator Guid(PetId petId) => petId.Id;
    public static implicit operator PetId(Guid id) => new(id);
    
}