using PawShelter.Application.Dto;

namespace PawShelter.Application.Volunteers.UpdateMainInfo;

public record UpdateMainInfoCommand(Guid VolunteerId, MainInfoDto MainInfoDto);
