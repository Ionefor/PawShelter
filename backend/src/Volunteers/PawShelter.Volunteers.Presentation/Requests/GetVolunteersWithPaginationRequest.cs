using PawShelter.Core.Dto.Queries;
using PawShelter.Volunteers.Application.Volunteers.Queries.GetVolunteersWithPagination;

namespace PawShelter.Volunteers.Presentation.Requests;

public record GetVolunteersWithPaginationRequest(PaginationParamsDto PaginationParams)
{
    public GetVolunteersWithPaginationQuery ToQuery()
    {
        return new GetVolunteersWithPaginationQuery(PaginationParams);
    }
}