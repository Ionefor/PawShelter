using PawShelter.Core.Abstractions;
using PawShelter.Volunteers.Contracts.Dto.Query;

namespace PawShelter.Volunteers.Application.Volunteers.Queries.GetFilteredPetsWithPagination;

public record GetFilteredPetsWithPaginationQuery(
    FilteringParamsDto? FilteringParams,
    SortingParamsDto? SortingParams,
    PaginationParamsDto PaginationParams) : IQuery;