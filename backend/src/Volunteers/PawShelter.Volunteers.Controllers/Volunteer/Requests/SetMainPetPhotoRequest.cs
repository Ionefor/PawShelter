using PawShelter.Application.Volunteers.UseCases.SetMainPetPhoto;

namespace PawShelter.Web.Controllers.Volunteer.Requests;

public record SetMainPetPhotoRequest(string FilePath)
{
    public SetMainPetPhotoCommand ToCommand(Guid volunteerId, Guid petId) =>
        new(volunteerId, petId, FilePath);
}