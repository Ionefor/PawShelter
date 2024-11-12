using PawShelter.Core.Abstractions;
using PawShelter.Core.Dto.Queries;

namespace PawShelter.Volunteers.Application.Volunteers.Queries.GetVolunteersWithPagination;

public record GetVolunteersWithPaginationQuery(PaginationParamsDto? PaginationParams) : IQuery;