namespace PawShelter.SharedKernel.ValueObjects;

public record SpeciesId
{
    private SpeciesId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static SpeciesId NewSpeciesId()
    {
        return new SpeciesId(Guid.NewGuid());
    }

    public static SpeciesId Empty()
    {
        return new SpeciesId(Guid.Empty);
    }

    public static SpeciesId Create(Guid id)
    {
        return new SpeciesId(id);
    }
}