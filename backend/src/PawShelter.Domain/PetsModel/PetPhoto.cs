
namespace PawShelter.Domain.PetsModel
{
    public record PetPhoto
    {
        public PetPhoto(string path, bool isMain)
        {
            Path = path;
            IsMain = isMain;
        }
        public string Path { get; } = null!;
        public bool IsMain { get; }
    }
}
