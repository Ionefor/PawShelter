using PawShelter.Volunteers.Application.Volunteers.Queries.GetVolunteersWithPagination;
using PawShelter.Volunteers.Contracts.Dto.Query;

namespace PawShelter.Volunteers.Presentation.Requests;

public record GetVolunteersWithPaginationRequest(PaginationParamsDto PaginationParams)
{
    public GetVolunteersWithPaginationQuery ToQuery()
    {
        return new GetVolunteersWithPaginationQuery(PaginationParams);
    }
}