using CSharpFunctionalExtensions;
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
        public static Result<SocialNetwork, Error> Create(Name name, string link)
        {
            if (string.IsNullOrWhiteSpace(link))
                return Errors.General.ValueIsInvalid("link");

            return new SocialNetwork(name, link);
        }
    }
}
