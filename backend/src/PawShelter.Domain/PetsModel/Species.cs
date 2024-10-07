using CSharpFunctionalExtensions;
using PawShelter.Domain.PetsModel.Ids;
using PawShelter.Domain.Shared;

namespace PawShelter.Domain.PetsModel
{
    public class Species : Shared.Entity<SpeciesId>
    {
        private readonly List<Breed> _breeds;
        private Species(SpeciesId id, string value) : base(id)
        {
            Value = value;
        }
        public string Value { get; private set; } = null!;
        public IReadOnlyList<Breed> Breeds => _breeds;
        public static Result<Species, Error> Create(SpeciesId id, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Errors.General.ValueIsInvalid("");

            return new Species(id, value);
        }
    }
}
