namespace PawShelter.Domain.Shared.ValueObjects
{
    public record Requisite
    {
        private Requisite()
        {
            
        }
        private Requisite(string name, string description)
        {
            Name = name;
            Description = description;
        }
        public string Name { get; }
        public string Description { get; }

        public Result<Requisite> Create(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description))            
                return "Invalid Requisite";
            
            return new Requisite(name, description);
        }
    }
}
