using PawShelter.Application.Volunteers.Delete;

namespace PawShelter.API.Controllers.Volunteer.Requests;

public record DeleteVolunteerRequest(Guid VolunteerId)
{
    public DeleteVolunteerCommand ToCommand() =>
        new(VolunteerId);
}