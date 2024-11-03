namespace PawShelter.Domain.PetsManagement.ValueObjects.Ids;

public record VolunteerId
{
    private VolunteerId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static VolunteerId NewVolonteerId()
    {
        return new VolunteerId(Guid.NewGuid());
    }

    public static VolunteerId Empty()
    {
        return new VolunteerId(Guid.Empty);
    }

    public static VolunteerId Create(Guid id)
    {
        return new VolunteerId(id);
    }

    public static implicit operator Guid(VolunteerId volunteerId)
    {
        return volunteerId.Value;
    }
}