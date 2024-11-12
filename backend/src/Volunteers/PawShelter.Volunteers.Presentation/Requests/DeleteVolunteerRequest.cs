using PawShelter.Volunteers.Application.Volunteers.Commands.Volunteer.Delete;

namespace PawShelter.Volunteers.Presentation.Requests;

public record DeleteVolunteerRequest(Guid VolunteerId)
{
    public DeleteVolunteerCommand ToCommand()
    {
        return new DeleteVolunteerCommand(VolunteerId);
    }
}