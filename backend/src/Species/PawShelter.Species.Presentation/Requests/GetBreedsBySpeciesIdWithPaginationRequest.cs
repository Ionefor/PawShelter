using PawShelter.Species.Application.Species.Queries.GetBreedsBySpeciesIdWithPagination;

namespace PawShelter.Species.Presentation.Requests;

public record GetBreedsBySpeciesIdWithPaginationRequest(int Page, int PageSize)
{
    public GetBreedsBySpeciesIdWithPaginationQuery ToQuery(Guid speciesId) =>
        new(speciesId, Page, PageSize);
}