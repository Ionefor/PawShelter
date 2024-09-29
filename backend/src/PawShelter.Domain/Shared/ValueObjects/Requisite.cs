namespace PawShelter.Domain.Shared.ValueObjects
{
    public record Requisite
    {
        private Requisite() { }
        private Requisite(Name name, Description description)
        {
            Name = name;
            Description = description;
        }
        public Name Name { get; }
        public Description Description { get; }
        public Result<Requisite> Create(Name name, Description description) =>
            new Requisite(name, description);
    }
}
