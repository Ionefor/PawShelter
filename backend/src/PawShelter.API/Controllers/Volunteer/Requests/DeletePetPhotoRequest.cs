using PawShelter.Application.Volunteers.UseCases.DeletePetPhoto;

namespace PawShelter.API.Controllers.Volunteer.Requests;

public record DeletePetPhotoRequest(string  FilePath)
{
    public DeletePetPhotoCommand ToCommand(Guid volunteerId, Guid petId) =>
        new(volunteerId, petId,  FilePath);
}