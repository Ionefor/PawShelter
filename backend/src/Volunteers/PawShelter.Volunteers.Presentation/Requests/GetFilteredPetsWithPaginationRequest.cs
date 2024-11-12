using PawShelter.Core.Dto.Queries;
using PawShelter.Volunteers.Application.Volunteers.Queries.GetFilteredPetsWithPagination;

namespace PawShelter.Volunteers.Presentation.Requests;

public record GetFilteredPetsWithPaginationRequest(
    FilteringParamsDto? FilteringParams,
    SortingParamsDto? SortingParams,
    PaginationParamsDto PaginationParams)
{
    public GetFilteredPetsWithPaginationQuery ToQuery() =>
        new(FilteringParams, SortingParams, PaginationParams);
}
