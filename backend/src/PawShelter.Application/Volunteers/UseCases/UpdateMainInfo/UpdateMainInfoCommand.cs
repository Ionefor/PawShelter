using PawShelter.Application.Abstractions;
using PawShelter.Application.Dto;

namespace PawShelter.Application.Volunteers.UseCases.UpdateMainInfo;

public record UpdateMainInfoCommand(Guid VolunteerId, MainInfoDto MainInfoDto) : ICommand;