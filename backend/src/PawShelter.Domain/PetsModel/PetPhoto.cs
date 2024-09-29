using PawShelter.Domain.Shared;

namespace PawShelter.Domain.PetsModel
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
        public Result<PetPhoto> Create(string path, bool isMain)
        {
            if (string.IsNullOrWhiteSpace(path))
                return "Invalid path";

            return new PetPhoto(path, isMain);
        }
    }
}
