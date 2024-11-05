using PawShelter.Application.Abstractions;

namespace PawShelter.Application.Species.Queries.GetBreedsBySpeciesIdWithPagination;

public record GetBreedsBySpeciesIdWithPaginationQuery(Guid SpeciesId, int Page, int PageSize) : IQuery;