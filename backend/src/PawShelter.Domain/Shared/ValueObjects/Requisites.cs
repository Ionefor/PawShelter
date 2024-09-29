namespace PawShelter.Domain.Shared.ValueObjects
{
    public record Requisites
    {
        private Requisites() { }
        public Requisites(List<Requisite> requisites) =>
             Values = requisites;
        public IReadOnlyList<Requisite>? Values { get; }
    }
}
