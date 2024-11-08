using PawShelter.Application.Dto.QueryDto;
using PawShelter.Application.Volunteers.Queries.GetFilteredPetsWithPagination;

namespace PawShelter.API.Controllers.Volunteer.Requests;

public record GetFilteredPetsWithPaginationRequest(
    FilteringParamsDto? FilteringParamsDto,
    SortingParamsDto? SortingParamsDto,
    PaginationParamsDto PaginationParamsDto)
{
    public GetFilteredPetsWithPaginationQuery ToQuery() =>
        new(FilteringParamsDto, SortingParamsDto, PaginationParamsDto);
}
