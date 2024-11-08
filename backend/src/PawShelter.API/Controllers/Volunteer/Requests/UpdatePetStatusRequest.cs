using PawShelter.Application.Volunteers.UseCases.UpdatePetStatus;

namespace PawShelter.API.Controllers.Volunteer.Requests;

public record UpdatePetStatusRequest(string PetStatus)
{
    public UpdatePetStatusCommand ToCommand(Guid voluneerId, Guid petId) =>
        new(voluneerId, petId, PetStatus);
}
