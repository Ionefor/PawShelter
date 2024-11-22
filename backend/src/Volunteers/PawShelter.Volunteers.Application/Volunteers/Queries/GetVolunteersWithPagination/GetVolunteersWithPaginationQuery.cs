using PawShelter.Core.Abstractions;
using PawShelter.Volunteers.Contracts.Dto.Query;

namespace PawShelter.Volunteers.Application.Volunteers.Queries.GetVolunteersWithPagination;

public record GetVolunteersWithPaginationQuery(PaginationParamsDto? PaginationParams) : IQuery;