using CSharpFunctionalExtensions;
using PawShelter.Domain.Shared;

namespace PawShelter.Domain.PetsModel
{
    public record HealthInfo
    {
        private const int MAX_LENGTH = 2000;
        private HealthInfo() { }
        private HealthInfo(string healthInfo)
        {
            Value = healthInfo;
        }
        public string Value { get; }
        public static Result<HealthInfo, Error> Create(string value)
        {
            if(string.IsNullOrWhiteSpace(value) || value.Length > MAX_LENGTH)
                return Errors.General.ValueIsInvalid("HealthInfo");

            return new HealthInfo(value);
        }
    }
}
