using PawShelter.Application.Dto;

namespace PawShelter.Application.Volunteers.UpdateMainInfo;

public record UpdateMainInfoRequest(MainInfoDto MainInfoDto)
{
    public UpdateMainInfoCommand ToCommand(Guid volunteerId) =>
        new(volunteerId, MainInfoDto);
}