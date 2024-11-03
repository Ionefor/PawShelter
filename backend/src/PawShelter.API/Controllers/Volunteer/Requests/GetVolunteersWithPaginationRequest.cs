using PawShelter.Application.Volunteers.Queries.GetVolunteersWithPagination;

namespace PawShelter.API.Controllers.Volunteer.Requests;

public record GetVolunteersWithPaginationRequest(int Page, int PageSize)
{
    public GetVolunteersWithPaginationQuery ToQuery()
    {
        return new GetVolunteersWithPaginationQuery(Page, PageSize);
    }
}