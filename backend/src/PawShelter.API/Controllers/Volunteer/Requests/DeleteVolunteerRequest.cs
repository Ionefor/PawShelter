using PawShelter.Application.Volunteers.UseCases.Delete;

namespace PawShelter.API.Controllers.Volunteer.Requests;

public record DeleteVolunteerRequest(Guid VolunteerId)
{
    public DeleteVolunteerCommand ToCommand()
    {
        return new DeleteVolunteerCommand(VolunteerId);
    }
}