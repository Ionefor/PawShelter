using PawShelter.Application.Dto.QueryDto;
using PawShelter.Application.Volunteers.Queries.GetFilteredPetsWithPagination;

namespace PawShelter.Web.Controllers.Volunteer.Requests;

public record GetFilteredPetsWithPaginationRequest(
    FilteringParamsDto? FilteringParams,
    SortingParamsDto? SortingParams,
    PaginationParamsDto PaginationParams)
{
    public GetFilteredPetsWithPaginationQuery ToQuery() =>
        new(FilteringParams, SortingParams, PaginationParams);
}
