using PawShelter.Application.Abstractions;
using PawShelter.Application.Dto.QueryDto;

namespace PawShelter.Application.Volunteers.Queries.GetFilteredPetsWithPagination;

public record GetFilteredPetsWithPaginationQuery(
    FilteringParamsDto? FilteringParamsDto,
    SortingParamsDto? SortingParamsDto,
    PaginationParamsDto PaginationParamsDto) : IQuery;