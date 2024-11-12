using PawShelter.Core.Abstractions;

namespace PawShelter.Volunteers.Application.Volunteers.Queries.GetPetById;

public record GetPetByIdQuery(Guid PetId) : IQuery;
