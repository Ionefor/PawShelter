namespace PawShelter.SharedKernel.ValueObjects;

public record Requisites
{
    private Requisites()
    {
    }

    public Requisites(IEnumerable<Requisite> requisites)
    {
        Values = requisites.ToList();
    }

    public IReadOnlyList<Requisite>? Values { get; }
}