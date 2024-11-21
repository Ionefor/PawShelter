using PawShelter.SharedKernel.Models.Abstractions;

namespace PawShelter.SharedKernel.ValueObjects.Ids;

public class VolunteerId : BaseId<VolunteerId>
{
    private VolunteerId(VolunteerId id) : base(id) {}
    public static implicit operator Guid(VolunteerId volunteerId) => volunteerId.Id;
    public static implicit operator VolunteerId(Guid id) => new(id);
}