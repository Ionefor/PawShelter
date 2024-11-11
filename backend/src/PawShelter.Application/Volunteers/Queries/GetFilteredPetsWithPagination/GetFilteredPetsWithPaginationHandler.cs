using PawShelter.Application.Abstractions;
using PawShelter.Application.Database;
using PawShelter.Application.Dto;
using PawShelter.Application.Extensions;
using PawShelter.Application.Models;

namespace PawShelter.Application.Volunteers.Queries.GetFilteredPetsWithPagination;

public class GetFilteredPetsWithPaginationHandler : 
    IQueryHandler<PageList<PetDto>, GetFilteredPetsWithPaginationQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetFilteredPetsWithPaginationHandler(
        IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    public async Task<PageList<PetDto>> Handle(
        GetFilteredPetsWithPaginationQuery query,
        CancellationToken cancellationToken = default)
    {
        var petsQuery = _readDbContext.Pets;
        
        #region Filtering
        
            if (query.FilteringParams is not null)
            {
                petsQuery.WhereIf(
                    query.FilteringParams!.VolunteerId != null,
                    q => q.VolunteerId == query.FilteringParams.VolunteerId);
                
                petsQuery.WhereIf(
                    !string.IsNullOrWhiteSpace(query.FilteringParams!.Name),
                    q => q.Name.Contains(query.FilteringParams.Name!));
                
                petsQuery.WhereIf(
                    !string.IsNullOrWhiteSpace(query.FilteringParams!.Color),
                    q => q.Color.Contains(query.FilteringParams.Color!));
                
                petsQuery.WhereIf(
                    query.FilteringParams!.SpeciesId != null,
                    q => q.SpeciesId == query.FilteringParams.SpeciesId);
                
                petsQuery.WhereIf(
                    query.FilteringParams!.BreedId != null,
                    q => q.BreedId == query.FilteringParams.BreedId);
        
                if (query.FilteringParams.PetCharacteristics is not null)
                {
                    petsQuery = petsQuery.Where(
                            q => q.Weight.Equals(
                                     query.FilteringParams.PetCharacteristics.Weight) &&
                                 q.Height.Equals(
                                     query.FilteringParams.PetCharacteristics.Height));
                }
                
                petsQuery.WhereIf(
                    query.FilteringParams!.BirthDate != null,
                    q => q.Birthday == query.FilteringParams.BirthDate);
        
                if (query.FilteringParams.Address is not null)
                {
                    petsQuery = petsQuery.Where(
                        q => q.Address.Country == query.FilteringParams.Address.Country &&
                             q.Address.City == query.FilteringParams.Address.City &&
                             q.Address.Street == query.FilteringParams.Address.Street &&
                             q.Address.HouseNumber == query.FilteringParams.Address.HouseNumber);
                }
            }
            
        #endregion

        #region Sorting
        
            if (query.SortingParams is not null)
            {
                petsQuery.SortIf(
                    query.SortingParams.VolunteerId is true,
                        q => q.VolunteerId);
                
                petsQuery.SortIf(
                    query.SortingParams.Name is true,
                    q => q.Name);
                
                petsQuery.SortIf(
                    query.SortingParams.Color is true,
                    q => q.Color);
                
                petsQuery.SortIf(
                    query.SortingParams.Species is true,
                    q => q.SpeciesId);
                
                petsQuery.SortIf(
                    query.SortingParams.Breed is true,
                    q => q.BreedId);
        
                if (query.SortingParams.PetCharacteristics is true)
                {
                    petsQuery = petsQuery.
                        OrderBy(q => q.Weight);
                    
                    petsQuery = petsQuery.
                        OrderBy(q => q.Height);
                }
                
                petsQuery.SortIf(
                    query.SortingParams.BirthDate is true,
                    q => q.Birthday);
        
                if (query.SortingParams.Address is not null)
                {
                    petsQuery = petsQuery.
                        OrderBy(q => q.Address.Country);
                
                    petsQuery = petsQuery.
                        OrderBy(q => q.Address.City);
                
                    petsQuery = petsQuery.
                        OrderBy(q => q.Address.Street);
                    
                    petsQuery = petsQuery.
                        OrderBy(q => q.Address.HouseNumber);
                }
            }
            
        #endregion
        
        return await petsQuery.ToPagedList(
            query.PaginationParams.Page,
                query.PaginationParams.PageSize,
                cancellationToken);
    }
}