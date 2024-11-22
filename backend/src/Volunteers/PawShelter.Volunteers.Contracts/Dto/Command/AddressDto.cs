namespace PawShelter.Volunteers.Contracts.Dto.Command;

public record AddressDto(
    string Country,
    string City,
    string Street,
    string HouseNumber);