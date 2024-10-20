﻿using PawShelter.Application.Dto;

namespace PawShelter.Application.Volunteers.Create
{
    public record CreateVolunteerCommand(
        FullNameDto FullNameDto,
        string Description,
        string Email,
        string PhoneNumber,
        int Experience,
        IEnumerable<RequisiteDto>? Requisites,
        IEnumerable<SocialNetworkDto>? SocialNetworks);
}
