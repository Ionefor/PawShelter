using PawShelter.Application.Dto;
using PawShelter.Application.Volunteers.UseCases.UpdateMainInfo;

namespace PawShelter.Web.Controllers.Volunteer.Requests;

public record UpdateMainInfoRequest(MainInfoDto MainInfoDto)
{
    public UpdateMainInfoCommand ToCommand(Guid volunteerId)
    {
        return new UpdateMainInfoCommand(volunteerId, MainInfoDto);
    }
}