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
        public Result<Color> Create(string color)
        {
            if (string.IsNullOrWhiteSpace(color))
                return "Invalid color";

            return new Color(color);
        }
    }
}
