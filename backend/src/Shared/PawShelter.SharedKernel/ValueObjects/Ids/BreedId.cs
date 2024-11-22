using PawShelter.SharedKernel.Models.Abstractions;

namespace PawShelter.SharedKernel.ValueObjects.Ids;

public class BreedId(Guid id) : BaseId<BreedId>(id)
{
    public static implicit operator Guid(BreedId breedId) => breedId.Id;
    public static implicit operator BreedId(Guid id) => new(id);
}