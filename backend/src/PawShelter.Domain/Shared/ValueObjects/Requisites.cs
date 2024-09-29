namespace PawShelter.Domain.Shared.ValueObjects
{
    public record Requisites
    {
        public IReadOnlyList<Requisite>? Values { get; }
    }
}
