namespace PawShelter.Domain.VolunteerModel
{
    public class SocialNetworks
    {
        private SocialNetworks() { }
        public SocialNetworks(List<SocialNetwork> socialNetworks) =>
            Values = socialNetworks;
        public IReadOnlyList<SocialNetwork>? Values { get; }
    }
}
