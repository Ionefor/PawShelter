using PawShelter.Species.Application.Species.Queries.GetSpeciesWithPagination;

namespace PawShelter.Species.Presentation.Requests;

public record GetSpeciesWithPaginationRequest(int Page, int PageSize)
{
    public GetSpeciesWithPaginationQuery ToQuery() =>
     new(Page, PageSize);
}