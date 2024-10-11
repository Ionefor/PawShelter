namespace PawShelter.Domain.PetsManagement.ValueObjects.ForPet
{
    public record Photos
    {
        private Photos() { }
        public Photos(List<PetPhoto> photos) =>
            Values = photos;
        public IReadOnlyList<PetPhoto>? Values { get; }
    }
}
