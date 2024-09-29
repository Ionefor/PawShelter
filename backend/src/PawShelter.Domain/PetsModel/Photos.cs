namespace PawShelter.Domain.PetsModel
{
    public record Photos
    {
        public IReadOnlyList<PetPhoto>? Values { get; }
    }
}
