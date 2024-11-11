using PawShelter.Application.Species.Queries.GetBreedsBySpeciesIdWithPagination;

namespace PawShelter.Web.Controllers.Species.Requests;

public record GetBreedsBySpeciesIdWithPaginationRequest(int Page, int PageSize)
{
    public GetBreedsBySpeciesIdWithPaginationQuery ToQuery(Guid speciesId) =>
        new(speciesId, Page, PageSize);
}