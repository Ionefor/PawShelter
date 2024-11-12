using PawShelter.Volunteers.Application.Volunteers.Commands.Pet.DeletePetPhoto;

namespace PawShelter.Volunteers.Presentation.Requests;

public record DeletePetPhotoRequest(string  FilePath)
{
    public DeletePetPhotoCommand ToCommand(Guid volunteerId, Guid petId) =>
        new(volunteerId, petId,  FilePath);
}