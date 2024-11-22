using CSharpFunctionalExtensions;
using PawShelter.SharedKernel.Models.Error;

namespace PawShelter.SharedKernel.ValueObjects;

public class Requisite : ComparableValueObject
{
    private Requisite() {}
    private Requisite(string name, string description)
    {
        Name = name;
        Description = description;
    }
    public string Name { get; }
    public string Description { get; }

    public static Result<Requisite, Error> Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name) ||
            string.IsNullOrWhiteSpace(description))
        {
            return Errors.General.
                ValueIsInvalid(new ErrorParameters.General.ValueIsInvalid(nameof(Requisite)));
        }
            
        return new Requisite(name, description);
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Name;
        yield return Description;
    }
}