using PawShelter.Application.Volunteers.UseCases.Delete;

namespace PawShelter.Web.Controllers.Volunteer.Requests;

public record DeleteVolunteerRequest(Guid VolunteerId)
{
    public DeleteVolunteerCommand ToCommand()
    {
        return new DeleteVolunteerCommand(VolunteerId);
    }
}