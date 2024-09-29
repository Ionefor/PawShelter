using PawShelter.Domain.Shared;

namespace PawShelter.Domain.VolunteerModel
{
    public record SocialNetwork
    {
        private SocialNetwork() { }
        private SocialNetwork(string name, string link)
        {
            Name = name;
            Link = link;
        }
        public string Name { get; }
        public string Link { get; }
        public Result<SocialNetwork> Create(string name, string link)
        {
            if (string.IsNullOrWhiteSpace(link) || string.IsNullOrWhiteSpace(name))
                return "Invalid info";

            return new SocialNetwork(name, link);
        }
    }
}
