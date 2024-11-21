﻿using CSharpFunctionalExtensions;
using PawShelter.SharedKernel.Models.Error;

namespace PawShelter.Volunteers.Domain.ValueObjects;

public class Address : ComparableValueObject
{
    private Address() {}
    private Address(string country, string city,
        string street, string houseNumber)
    {
        Country = country;
        City = city;
        Street = street;
        HouseNumber = houseNumber;
    }
    public string Country { get; }
    public string City { get; }
    public string Street { get; }
    public string HouseNumber { get; }

    public static Result<Address, Error> Create(
        string country, string city, string street, string houseNumber)
    {
        if (string.IsNullOrWhiteSpace(country) || string.IsNullOrWhiteSpace(city) ||
            string.IsNullOrWhiteSpace(street) || string.IsNullOrWhiteSpace(houseNumber))
        {
            return Errors.General.
                ValueIsInvalid(new ErrorParameters.General.ValueIsInvalid(nameof(Address)));
        }

        return new Address(country, city, street, houseNumber);
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Country;
        yield return City;
        yield return Street;
        yield return HouseNumber;
    }
}