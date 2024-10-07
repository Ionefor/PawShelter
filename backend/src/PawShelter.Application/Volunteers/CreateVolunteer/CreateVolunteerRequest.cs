using PawShelter.Application.Dto;
namespace PawShelter.Application.Volunteers.CreateVolunteer;

public record CreateVolunteerRequest(FullNameDto fullNameDto,
    string description, string email, string phoneNumber, int experience,
    List<RequisiteDto>? requisites, List<SocialNetworkDto>? socialNetworks);
