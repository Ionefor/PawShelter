using PawShelter.Volunteers.Application.Volunteers.Queries.GetFilteredPetsWithPagination;
using PawShelter.Volunteers.Contracts.Dto.Query;

namespace PawShelter.Volunteers.Presentation.Requests;

public record GetFilteredPetsWithPaginationRequest(
    FilteringParamsDto? FilteringParams,
    SortingParamsDto? SortingParams,
    PaginationParamsDto PaginationParams)
{
    public GetFilteredPetsWithPaginationQuery ToQuery() =>
        new(FilteringParams, SortingParams, PaginationParams);
}
