using PawShelter.Application.Abstractions;
using PawShelter.Application.Dto.QueryDto;

namespace PawShelter.Application.Volunteers.Queries.GetVolunteersWithPagination;

public record GetVolunteersWithPaginationQuery(PaginationParamsDto? PaginationParams) : IQuery;