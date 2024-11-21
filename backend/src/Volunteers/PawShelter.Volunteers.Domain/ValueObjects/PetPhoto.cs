using CSharpFunctionalExtensions;
using PawShelter.SharedKernel.ValueObjects;

namespace PawShelter.Volunteers.Domain.ValueObjects;

public class PetPhoto : ComparableValueObject
{
    private PetPhoto() {}
    public PetPhoto(FilePath path, bool isMain)
    {
        Path = path;
        IsMain = isMain;
    }
    public FilePath Path { get; } = null!;
    public bool IsMain { get; }
    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Path;
        yield return IsMain;
    }
}