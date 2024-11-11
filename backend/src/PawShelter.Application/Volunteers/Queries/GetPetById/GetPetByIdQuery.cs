using PawShelter.Application.Abstractions;

namespace PawShelter.Application.Volunteers.Queries.GetPetById;

public record GetPetByIdQuery(Guid PetId) : IQuery;
