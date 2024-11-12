using PawShelter.Core.Abstractions;
using PawShelter.Core.Dto.Queries;

namespace PawShelter.Volunteers.Application.Volunteers.Queries.GetFilteredPetsWithPagination;

public record GetFilteredPetsWithPaginationQuery(
    FilteringParamsDto? FilteringParams,
    SortingParamsDto? SortingParams,
    PaginationParamsDto PaginationParams) : IQuery;