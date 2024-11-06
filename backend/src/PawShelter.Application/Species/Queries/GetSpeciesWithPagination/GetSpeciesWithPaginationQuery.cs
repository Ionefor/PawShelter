using PawShelter.Application.Abstractions;

namespace PawShelter.Application.Species.Queries.GetSpeciesWithPagination;

public record GetSpeciesWithPaginationQuery(int Page, int PageSize) : IQuery;