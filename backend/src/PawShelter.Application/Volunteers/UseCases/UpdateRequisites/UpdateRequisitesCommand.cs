using PawShelter.Application.Abstractions;
using PawShelter.Application.Dto;

namespace PawShelter.Application.Volunteers.UseCases.UpdateRequisites;

public record UpdateRequisitesCommand(Guid VolunteerId, RequisitesDto RequisitesDto) : ICommand;