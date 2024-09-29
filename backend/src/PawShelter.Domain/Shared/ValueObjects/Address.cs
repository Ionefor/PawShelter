namespace PawShelter.Domain.Shared.ValueObjects
{
    public class Address
    {
        private Address()
        {
        }       
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
        public static Result<Address> Create(string country, string city,
            string street, string houseNumber)
        {
            if(string.IsNullOrWhiteSpace(country) || string.IsNullOrWhiteSpace(city) ||
                string.IsNullOrWhiteSpace(street) || string.IsNullOrWhiteSpace(houseNumber))
            {
                return "Invalid address";
            }
               
            return new Address(country, city, street, houseNumber);
        }
    }
}
