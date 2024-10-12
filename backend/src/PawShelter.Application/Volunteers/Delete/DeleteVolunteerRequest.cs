namespace PawShelter.Application.Volunteers.Delete;

public record DeleteVolunteerRequest(Guid VolunteerId)
{
    public DeleteVolunteerCommand ToCommand() =>
        new(VolunteerId);
}