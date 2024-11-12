using PawShelter.Core.Abstractions;
using PawShelter.Core.Dto;

namespace PawShelter.Volunteers.Application.Volunteers.Commands.Volunteer.UpdateMainInfo;

public record UpdateMainInfoCommand(Guid VolunteerId, MainInfoDto MainInfoDto) : ICommand;