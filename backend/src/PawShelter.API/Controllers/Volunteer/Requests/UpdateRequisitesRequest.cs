using PawShelter.Application.Dto;
using PawShelter.Application.Volunteers.UseCases.UpdateRequisites;

namespace PawShelter.API.Controllers.Volunteer.Requests;

public record UpdateRequisitesRequest(RequisitesDto RequisitesDto)
{
    public UpdateRequisitesCommand ToCommand(Guid volunteerId)
    {
        return new UpdateRequisitesCommand(volunteerId, RequisitesDto);
    }
}
