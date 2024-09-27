using PawShelter.Domain.Enums;
using PawShelter.Domain.PetsModel;
using PawShelter.Domain.Shared;

namespace PawShelter.Domain.Pets
{
    public class Pet
    {
        private readonly List<Requisites> _requisites = [];
        private readonly List<PetPhoto> _photos = [];
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public string Species { get; private set; } = null!;
        public string Breed { get; private set; } = null!;
        public string Color { get; private set; } = null!;
        public string HealthInfo { get; private set; } = null!;
        public string AddressLocated { get; private set; } = null!;
        public string NumberOfOwner { get; private set; } = null!;
        public double Height { get; private set; }
        public double Width { get; private set; }
        public bool IsCastrated { get; private set; }
        public bool IsVaccinated { get; private set; }
        public DateTime Birthday { get; private set; }
        public DateTime PublicationDate { get; private set; }
        public IReadOnlyList<Requisites> Requisites  => _requisites;
        public IReadOnlyList<PetPhoto> Photos => _photos;
        public PetStatus Status { get; private set; }    
    }
}
