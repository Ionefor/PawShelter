﻿using PawShelter.Application.Dto;
using PawShelter.Application.Volunteers.Create;

namespace PawShelter.Application.Volunteers.CreateVolunteer;

public record CreateVolunteerRequest(
             FullNameDto FullNameDto,
             string Description,
             string Email,
             string PhoneNumber,
             int Experience,
             IEnumerable<RequisiteDto>? Requisites,
             IEnumerable<SocialNetworkDto>? SocialNetworks)
{
    public CreateVolunteerCommand ToCommand() =>
            new(FullNameDto,
                Description,
                Email,
                PhoneNumber,
                Experience,
                Requisites,
                SocialNetworks);
}