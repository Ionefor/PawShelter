using CSharpFunctionalExtensions;
using PawShelter.SharedKernel.Models.Error;

namespace PawShelter.SharedKernel.ValueObjects;

public class SocialNetwork : ComparableValueObject
{
    private SocialNetwork() {}
    private SocialNetwork(string name, string url)
    {
        Name = name;
        Url = url;
    }
    public string Name { get; }
    public string Url { get; }

    public static Result<SocialNetwork, Error> Create(string name, string link)
    {
        if (string.IsNullOrWhiteSpace(link) || string.IsNullOrWhiteSpace(name))
        {
            return Errors.General.
                ValueIsInvalid(new ErrorParameters.General.ValueIsInvalid(nameof(SocialNetwork)));
        }
        
        return new SocialNetwork(name, link);
    }
    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Name;
        yield return Url;
    }
}