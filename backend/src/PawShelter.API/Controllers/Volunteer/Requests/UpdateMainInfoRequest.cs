using PawShelter.Application.Dto;
using PawShelter.Application.Volunteers.UpdateMainInfo;

namespace PawShelter.API.Controllers.Volunteer.Requests;

public record UpdateMainInfoRequest(MainInfoDto MainInfoDto)
{
    public UpdateMainInfoCommand ToCommand(Guid volunteerId) =>
        new(volunteerId, MainInfoDto);
}