using PawShelter.SharedKernel.Models.Abstractions;

namespace PawShelter.SharedKernel.ValueObjects.Ids;

public class VolunteerId(Guid id) : BaseId<VolunteerId>(id)
{
    public static implicit operator Guid(VolunteerId volunteerId) => volunteerId.Id;
    public static implicit operator VolunteerId(Guid id) => new(id);
}