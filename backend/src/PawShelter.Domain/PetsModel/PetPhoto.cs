
namespace PawShelter.Domain.PetsModel
{
    public record PetPhoto
    {
        public string Path { get; } = null!;
        public bool IsMain { get; }
    }
}
