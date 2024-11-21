using CSharpFunctionalExtensions;
using PawShelter.SharedKernel.ValueObjects.Ids;

namespace PawShelter.SharedKernel.ValueObjects;

public class SpeciesBreedsId : ComparableValueObject
{
    private SpeciesBreedsId() {}
    public SpeciesBreedsId(SpeciesId speciesId, Guid breedId)
    {
        SpeciesId = speciesId;
        BreedId = breedId;
    }
    public SpeciesId SpeciesId { get; }
    public Guid BreedId { get; }
    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return SpeciesId;
        yield return BreedId;
    }
}