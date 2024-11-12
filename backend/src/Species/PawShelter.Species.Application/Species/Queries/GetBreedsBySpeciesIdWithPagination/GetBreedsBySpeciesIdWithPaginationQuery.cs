using PawShelter.Core.Abstractions;

namespace PawShelter.Species.Application.Species.Queries.GetBreedsBySpeciesIdWithPagination;

public record GetBreedsBySpeciesIdWithPaginationQuery(Guid SpeciesId, int Page, int PageSize) : IQuery;