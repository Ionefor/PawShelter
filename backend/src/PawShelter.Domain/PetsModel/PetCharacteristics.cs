using PawShelter.Domain.Shared;

namespace PawShelter.Domain.PetsModel
{
    public record PetCharacteristics
    {
        private PetCharacteristics() { }
        private PetCharacteristics(double height, double width)
        {
            Height = height;
            Width = width;
        }
        public double Height { get; }
        public double Width { get; }
        public Result<PetCharacteristics> Create(double height, double width)
        {
            if (height <= 0 || height > 400 || width <= 0 || width > 400)
                return "Invalid characteristics";

            return new PetCharacteristics(Height, Width);
        }
    }
}
