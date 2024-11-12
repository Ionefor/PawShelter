using PawShelter.Core.Abstractions;

namespace PawShelter.Species.Application.Species.Queries.GetSpeciesWithPagination;

public record GetSpeciesWithPaginationQuery(int Page, int PageSize) : IQuery;