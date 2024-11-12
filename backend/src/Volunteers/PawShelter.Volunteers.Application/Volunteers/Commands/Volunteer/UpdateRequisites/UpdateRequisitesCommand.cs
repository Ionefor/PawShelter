using PawShelter.Core.Abstractions;
using PawShelter.Core.Dto;

namespace PawShelter.Volunteers.Application.Volunteers.Commands.Volunteer.UpdateRequisites;

public record UpdateRequisitesCommand(Guid VolunteerId, RequisitesDto RequisitesDto) : ICommand;