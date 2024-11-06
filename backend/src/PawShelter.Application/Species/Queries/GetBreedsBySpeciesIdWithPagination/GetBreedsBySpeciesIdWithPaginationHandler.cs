using PawShelter.Application.Abstractions;
using PawShelter.Application.Database;
using PawShelter.Application.Dto;
using PawShelter.Application.Extensions;
using PawShelter.Application.Models;

namespace PawShelter.Application.Species.Queries.GetBreedsBySpeciesIdWithPagination;

public class GetBreedsBySpeciesIdWithPaginationHandler :
    IQueryHandler<PageList<BreedDto>,
        GetBreedsBySpeciesIdWithPaginationQuery>
{
    private readonly IReadDbContext _context;

    public GetBreedsBySpeciesIdWithPaginationHandler(
        IReadDbContext context)
    {
        _context = context;
    }
    
    public async Task<PageList<BreedDto>> Handle(
        GetBreedsBySpeciesIdWithPaginationQuery query,
        CancellationToken cancellationToken = default)
    {
         var breedsQuery = _context.Breeds.Where(b => b.SpeciesId == query.SpeciesId);

        return await  breedsQuery.ToPagedList(
            query.Page, query.PageSize, cancellationToken);
    }
}