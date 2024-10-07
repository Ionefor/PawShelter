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
        public Result<Color, Error> Create(string color)
        {
            if (string.IsNullOrWhiteSpace(color))
                return Errors.General.ValueIsInvalid("Color");

            return new Color(color);
        }
    }
}
