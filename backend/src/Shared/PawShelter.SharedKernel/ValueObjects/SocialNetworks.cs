using CSharpFunctionalExtensions;

namespace PawShelter.SharedKernel.ValueObjects;

public class SocialNetworks : ComparableValueObject
{
    private SocialNetworks() {}
    public SocialNetworks(IEnumerable<SocialNetwork> socialNetworks)
    {
        Values = socialNetworks.ToList();
    }
    public IReadOnlyList<SocialNetwork>? Values { get; }
    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Values!.Single();
    }
}