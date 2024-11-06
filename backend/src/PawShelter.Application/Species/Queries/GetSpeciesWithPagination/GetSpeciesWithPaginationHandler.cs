using PawShelter.Application.Abstractions;
using PawShelter.Application.Database;
using PawShelter.Application.Dto;
using PawShelter.Application.Extensions;
using PawShelter.Application.Models;

namespace PawShelter.Application.Species.Queries.GetSpeciesWithPagination;

public class GetSpeciesWithPaginationHandler : 
    IQueryHandler<PageList<SpeciesDto>, GetSpeciesWithPaginationQuery>
{
    private readonly IReadDbContext _context;

    public GetSpeciesWithPaginationHandler(IReadDbContext context)
    {
        _context = context;
    }
    public async Task<PageList<SpeciesDto>> Handle(
        GetSpeciesWithPaginationQuery query,
        CancellationToken cancellationToken = default)
    {
        var speciesQuery = _context.Species;
        
        return await speciesQuery.ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
}