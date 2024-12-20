﻿using PawShelter.Core.Dto;
using PawShelter.Volunteers.Application.Volunteers.Commands.Volunteer.UpdateRequisites;
using PawShelter.Volunteers.Contracts.Dto.Command;

namespace PawShelter.Volunteers.Presentation.Requests;

public record UpdateRequisitesRequest(RequisitesDto RequisitesDto)
{
    public UpdateRequisitesCommand ToCommand(Guid volunteerId)
    {
        return new UpdateRequisitesCommand(volunteerId, RequisitesDto);
    }
}
