using PawShelter.Application.Dto;
using PawShelter.Application.Volunteers.UpdateRequisites;

namespace PawShelter.API.Controllers.Volunteer.Requests;

public record UpdateRequisitesRequest(RequisitesDto RequisitesDto)
{
    public UpdateRequisitesCommand ToCommand(Guid volunteerId) =>
        new(volunteerId, RequisitesDto);
}
