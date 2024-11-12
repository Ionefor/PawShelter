using PawShelter.Volunteers.Application.Volunteers.Commands.Pet.SetMainPetPhoto;

namespace PawShelter.Volunteers.Presentation.Requests;

public record SetMainPetPhotoRequest(string FilePath)
{
    public SetMainPetPhotoCommand ToCommand(Guid volunteerId, Guid petId) =>
        new(volunteerId, petId, FilePath);
}