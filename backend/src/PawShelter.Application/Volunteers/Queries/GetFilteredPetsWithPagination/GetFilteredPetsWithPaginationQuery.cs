using PawShelter.Application.Abstractions;
using PawShelter.Application.Dto.QueryDto;

namespace PawShelter.Application.Volunteers.Queries.GetFilteredPetsWithPagination;

public record GetFilteredPetsWithPaginationQuery(
    FilteringParamsDto? FilteringParams,
    SortingParamsDto? SortingParams,
    PaginationParamsDto PaginationParams) : IQuery;