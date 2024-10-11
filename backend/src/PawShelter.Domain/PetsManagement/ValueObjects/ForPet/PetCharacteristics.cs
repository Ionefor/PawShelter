using CSharpFunctionalExtensions;
using PawShelter.Domain.Shared;

namespace PawShelter.Domain.PetsManagement.ValueObjects.ForPet
{
    public record PetCharacteristics
    {
        private const int MIN_VALUE = 0;
        private const int MAX_VALUE = 400;
        private PetCharacteristics() { }
        private PetCharacteristics(double height, double width)
        {
            Height = height;
            Width = width;
        }
        public double Height { get; }
        public double Width { get; }
        public static Result<PetCharacteristics, Error> Create(double height, double width)
        {
            if (height <= MIN_VALUE || height > MAX_VALUE ||
                width <= MIN_VALUE || width > MAX_VALUE)
            {
                return Errors.General.ValueIsInvalid("PetCharacteristics");
            }
                
            return new PetCharacteristics(height, width);
        }
    }
}
