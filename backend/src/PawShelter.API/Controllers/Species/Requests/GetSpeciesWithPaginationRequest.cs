﻿using PawShelter.Application.Species.Queries;
using PawShelter.Application.Species.Queries.GetSpeciesWithPagination;

namespace PawShelter.API.Controllers.Species.Requests;

public record GetSpeciesWithPaginationRequest(int Page, int PageSize)
{
    public GetSpeciesWithPaginationQuery ToQuery() =>
     new(Page, PageSize);
}