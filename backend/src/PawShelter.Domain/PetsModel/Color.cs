using CSharpFunctionalExtensions;
using PawShelter.Domain.Shared;

namespace PawShelter.Domain.PetsModel
{
    public record Color
    {
        private Color() { }
        private Color(string color)
        {
            Value = color;
        }
        public string Value { get; }
        public static Result<Color, Error> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Errors.General.ValueIsInvalid("Color");

            return new Color(value);
        }
    }
}
