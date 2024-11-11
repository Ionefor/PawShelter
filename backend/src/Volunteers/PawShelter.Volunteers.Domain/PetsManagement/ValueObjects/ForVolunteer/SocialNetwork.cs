using CSharpFunctionalExtensions;
using PawShelter.Domain.Shared;

namespace PawShelter.Domain.PetsManagement.ValueObjects.ForVolunteer;

public record SocialNetwork
{
    private SocialNetwork()
    {
    }

    private SocialNetwork(string name, string link)
    {
        Name = name;
        Link = link;
    }

    public string Name { get; }
    public string Link { get; }

    public static Result<SocialNetwork, Error> Create(string name, string link)
    {
        if (string.IsNullOrWhiteSpace(link) || string.IsNullOrWhiteSpace(name))
            return Errors.General.ValueIsInvalid("link");

        return new SocialNetwork(name, link);
    }
}