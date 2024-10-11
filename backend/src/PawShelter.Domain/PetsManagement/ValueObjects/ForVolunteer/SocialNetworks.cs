namespace PawShelter.Domain.PetsManagement.ValueObjects.ForVolunteer
{
    public record SocialNetworks
    {
        private SocialNetworks() { }
        public SocialNetworks(List<SocialNetwork> socialNetworks) => Values = socialNetworks;
        public IReadOnlyList<SocialNetwork>? Values { get; }
    }
}
