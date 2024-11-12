using PawShelter.Core.Abstractions;

namespace PawShelter.Volunteers.Application.Volunteers.Queries.GetVolunteerById;

public record GetVolunteerByIdQuery(Guid VolunteerId) : IQuery;
