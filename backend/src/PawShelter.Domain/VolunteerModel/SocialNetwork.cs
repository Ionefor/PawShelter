using PawShelter.Domain.Shared;
using PawShelter.Domain.Shared.ValueObjects;

namespace PawShelter.Domain.VolunteerModel
{
    public record SocialNetwork
    {
        private SocialNetwork() { }
        private SocialNetwork(Name name, string link)
        {
            Name = name;
            Link = link;
        }
        public Name Name { get; }
        public string Link { get; }
        public Result<SocialNetwork> Create(Name name, string link)
        {
            if (string.IsNullOrWhiteSpace(link))
                return "Invalid info";

            return new SocialNetwork(name, link);
        }
    }
}
