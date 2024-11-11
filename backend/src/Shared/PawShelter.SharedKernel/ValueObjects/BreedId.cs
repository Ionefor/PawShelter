namespace PawShelter.SharedKernel.ValueObjects;

public record BreedId
{
    private BreedId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static BreedId NewBreedId()
    {
        return new BreedId(Guid.NewGuid());
    }

    public static BreedId Empty()
    {
        return new BreedId(Guid.Empty);
    }

    public static BreedId Create(Guid id)
    {
        return new BreedId(id);
    }
}