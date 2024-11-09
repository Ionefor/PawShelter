﻿using PawShelter.Application.Dto.QueryDto;
using PawShelter.Application.Volunteers.Queries.GetVolunteersWithPagination;

namespace PawShelter.API.Controllers.Volunteer.Requests;

public record GetVolunteersWithPaginationRequest(PaginationParamsDto PaginationParams)
{
    public GetVolunteersWithPaginationQuery ToQuery()
    {
        return new GetVolunteersWithPaginationQuery(PaginationParams);
    }
}