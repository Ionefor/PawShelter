using CSharpFunctionalExtensions;
using PawShelter.Domain.Shared;

namespace PawShelter.Domain.PetsManagement.ValueObjects.ForPet
{
    public record PetPhoto
    {
        private PetPhoto() { }
        private PetPhoto(string path, bool isMain)
        {
            Path = path;
            IsMain = isMain;
        }
        public string Path { get; } = null!;
        public bool IsMain { get; }
        public static Result<PetPhoto, Error> Create(string path, bool isMain)
        {
            if (string.IsNullOrWhiteSpace(path))
                return Errors.General.ValueIsInvalid("Path");

            return new PetPhoto(path, isMain);
        }
    }
}
