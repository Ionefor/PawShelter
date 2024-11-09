using PawShelter.Application.Abstractions;
using PawShelter.Application.Database;
using PawShelter.Application.Dto;
using PawShelter.Application.Extensions;
using PawShelter.Application.Models;

namespace PawShelter.Application.Volunteers.Queries.GetVolunteersWithPagination;

public class GetVolunteersWithPaginationHandler : 
    IQueryHandler<PageList<VolunteerDto>, GetVolunteersWithPaginationQuery>
{
    private readonly IReadDbContext _context;

    public GetVolunteersWithPaginationHandler(IReadDbContext context)
    {
        _context = context;
    }

    public async Task<PageList<VolunteerDto>> Handle(
        GetVolunteersWithPaginationQuery query,
        CancellationToken cancellationToken)
    {
        var volunteersQuery = _context.Volunteers;

        return await volunteersQuery.ToPagedList(
            query.PaginationParams!.Page, query.PaginationParams.PageSize, cancellationToken);
    }
}