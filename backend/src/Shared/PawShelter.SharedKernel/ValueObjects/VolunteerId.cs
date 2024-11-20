namespace PawShelter.SharedKernel.ValueObjects;

public class VolunteerId : BaseId<VolunteerId>
{
    private VolunteerId(PetId id) : base(id) {}
    public static implicit operator Guid(VolunteerId volunteerId) => volunteerId.Id;
    public static implicit operator VolunteerId(Guid id) => new(id);
}