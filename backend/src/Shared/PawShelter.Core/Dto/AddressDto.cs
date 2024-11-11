namespace PawShelter.Core.Dto;

public record AddressDto(
    string Country,
    string City,
    string Street,
    string HouseNumber);