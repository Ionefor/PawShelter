using PawShelter.Application.Dto;

namespace PawShelter.Application.Volunteers.UpdateRequisites;

public record UpdateRequisitesRequest(RequisitesDto RequisitesDto)
{
    public UpdateRequisitesCommand ToCommand(Guid volunteerId) =>
        new(volunteerId, RequisitesDto);
}
