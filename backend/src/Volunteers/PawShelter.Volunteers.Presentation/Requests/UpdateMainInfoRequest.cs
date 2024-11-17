﻿using PawShelter.Core.Dto;
using PawShelter.Volunteers.Application.Volunteers.Commands.Volunteer.UpdateMainInfo;

namespace PawShelter.Volunteers.Presentation.Requests;

public record UpdateMainInfoRequest(MainInfoDto MainInfoDto)
{
    public UpdateMainInfoCommand ToCommand(Guid volunteerId)
    {
        return new UpdateMainInfoCommand(volunteerId, MainInfoDto);
    }
}