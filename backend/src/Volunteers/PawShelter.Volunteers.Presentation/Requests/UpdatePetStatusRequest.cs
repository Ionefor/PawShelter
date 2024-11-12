using PawShelter.Volunteers.Application.Volunteers.Commands.Pet.UpdatePetStatus;

namespace PawShelter.Volunteers.Presentation.Requests;

public record UpdatePetStatusRequest(string PetStatus)
{
    public UpdatePetStatusCommand ToCommand(Guid voluneerId, Guid petId) =>
        new(voluneerId, petId, PetStatus);
}
