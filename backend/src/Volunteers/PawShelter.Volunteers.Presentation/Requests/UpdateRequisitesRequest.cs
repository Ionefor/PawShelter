using PawShelter.Core.Dto;
using PawShelter.Volunteers.Application.Volunteers.Commands.Volunteer.UpdateRequisites;

namespace PawShelter.Volunteers.Presentation.Requests;

public record UpdateRequisitesRequest(RequisitesDto RequisitesDto)
{
    public UpdateRequisitesCommand ToCommand(Guid volunteerId)
    {
        return new UpdateRequisitesCommand(volunteerId, RequisitesDto);
    }
}
