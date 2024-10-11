namespace PawShelter.Domain.PetsManagement.ValueObjects.Shared
{
    public record Requisites
    {
        private Requisites() { }
        public Requisites(List<Requisite> requisites) =>
             Values = requisites;
        public IReadOnlyList<Requisite>? Values { get; }
    }
}
