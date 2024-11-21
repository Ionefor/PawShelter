using CSharpFunctionalExtensions;

namespace PawShelter.SharedKernel.ValueObjects;

public class Requisites : ComparableValueObject
{
    private Requisites() {}

    public Requisites(IEnumerable<Requisite> requisites)
    {
        Values = requisites.ToList();
    }
    public IReadOnlyList<Requisite>? Values { get; }
    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Values.Single();
    }
}