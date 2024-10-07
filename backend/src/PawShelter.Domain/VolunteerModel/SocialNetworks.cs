namespace PawShelter.Domain.VolunteerModel
{
    public record SocialNetworks
    {
        private SocialNetworks() { }
        public SocialNetworks(List<SocialNetwork> socialNetworks) => Values = socialNetworks;
        public IReadOnlyList<SocialNetwork>? Values { get; }
    }
}
