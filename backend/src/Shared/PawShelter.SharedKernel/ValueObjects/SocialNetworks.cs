namespace PawShelter.SharedKernel.ValueObjects;

public record SocialNetworks
{
    private SocialNetworks()
    {
    }

    public SocialNetworks(IEnumerable<SocialNetwork> socialNetworks)
    {
        Values = socialNetworks.ToList();
    }

    public IReadOnlyList<SocialNetwork>? Values { get; }
}