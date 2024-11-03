using PawShelter.Application.Abstractions;

namespace PawShelter.Application.Volunteers.Queries.GetVolunteersWithPagination;

public record GetVolunteersWithPaginationQuery(int Page, int PageSize) : IQuery;