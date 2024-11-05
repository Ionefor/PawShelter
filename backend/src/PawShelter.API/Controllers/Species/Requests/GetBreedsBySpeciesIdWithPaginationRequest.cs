using PawShelter.Application.Species.Queries.GetBreedsBySpeciesIdWithPagination;

namespace PawShelter.API.Controllers.Species.Requests;

public record GetBreedsBySpeciesIdWithPaginationRequest(int Page, int PageSize)
{
    public GetBreedsBySpeciesIdWithPaginationQuery ToQuery(Guid speciesId) =>
        new(speciesId, Page, PageSize);
}