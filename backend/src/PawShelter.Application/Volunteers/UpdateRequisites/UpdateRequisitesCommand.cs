using PawShelter.Application.Dto;

namespace PawShelter.Application.Volunteers.UpdateRequisites;

public record UpdateRequisitesCommand(Guid VolunteerId, RequisitesDto RequisitesDto);
