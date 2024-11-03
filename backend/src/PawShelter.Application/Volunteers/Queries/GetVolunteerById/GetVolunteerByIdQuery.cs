using PawShelter.Application.Abstractions;

namespace PawShelter.Application.Volunteers.Queries.GetVolunteerById;

public record GetVolunteerByIdQuery(Guid VolunteerId) : IQuery;
