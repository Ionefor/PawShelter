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

            if (query.FilteringParamsDto is not null)
            {
                petsQuery.WhereIf(
                    query.FilteringParamsDto!.VolunteerId != null,
                    q => q.VolunteerId == query.FilteringParamsDto.VolunteerId);
                
                petsQuery.WhereIf(
                    !string.IsNullOrWhiteSpace(query.FilteringParamsDto!.Name),
                    q => q.Name.Contains(query.FilteringParamsDto.Name!));
                
                petsQuery.WhereIf(
                    !string.IsNullOrWhiteSpace(query.FilteringParamsDto!.Color),
                    q => q.Color.Contains(query.FilteringParamsDto.Color!));
                
                petsQuery.WhereIf(
                    query.FilteringParamsDto!.SpeciesId != null,
                    q => q.SpeciesId == query.FilteringParamsDto.SpeciesId);
                
                petsQuery.WhereIf(
                    query.FilteringParamsDto!.BreedId != null,
                    q => q.BreedId == query.FilteringParamsDto.BreedId);

                if (query.FilteringParamsDto.PetCharacteristics is not null)
                {
                    petsQuery = petsQuery.Where(
                            q => q.Weight.Equals(
                                     query.FilteringParamsDto.PetCharacteristics.Weight) &&
                                 q.Height.Equals(
                                     query.FilteringParamsDto.PetCharacteristics.Height));
                }
                
                petsQuery.WhereIf(
                    query.FilteringParamsDto!.BirthDate != null,
                    q => q.Birthday == query.FilteringParamsDto.BirthDate);

                if (query.FilteringParamsDto.Address is not null)
                {
                    petsQuery = petsQuery.Where(
                        q => q.Address.Country == query.FilteringParamsDto.Address.Country &&
                             q.Address.City == query.FilteringParamsDto.Address.City &&
                             q.Address.Street == query.FilteringParamsDto.Address.Street &&
                             q.Address.HouseNumber == query.FilteringParamsDto.Address.HouseNumber);
                }
            }
            
        #endregion

        #region Sorting

            if (query.SortingParamsDto is not null)
            {
                petsQuery.SortIf(
                    query.SortingParamsDto.VolunteerId is true,
                        q => q.VolunteerId);
                
                petsQuery.SortIf(
                    query.SortingParamsDto.Name is true,
                    q => q.Name);
                
                petsQuery.SortIf(
                    query.SortingParamsDto.Color is true,
                    q => q.Color);
                
                petsQuery.SortIf(
                    query.SortingParamsDto.Species is true,
                    q => q.SpeciesId);
                
                petsQuery.SortIf(
                    query.SortingParamsDto.Breed is true,
                    q => q.BreedId);

                if (query.SortingParamsDto.PetCharacteristics is not null)
                {
                    petsQuery = petsQuery.
                        OrderBy(q => q.Weight);
                    
                    petsQuery = petsQuery.
                        OrderBy(q => q.Height);
                }
                
                petsQuery.SortIf(
                    query.SortingParamsDto.BirthDate is true,
                    q => q.Birthday);

                if (query.SortingParamsDto.Address is not null)
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
            query.PaginationParamsDto.Page,
            query.PaginationParamsDto.PageSize, cancellationToken);
    }
}