using PawShelter.Domain.Enums;
using PawShelter.Domain.PetsModel;
using PawShelter.Domain.PetsModel.Ids;
using PawShelter.Domain.Shared;
using PawShelter.Domain.Shared.ValueObjects;

namespace PawShelter.Domain.Pets
{
    public class Pet : Entity<PetId>
    {
        private Pet(PetId id) : base(id) { }

        public Pet(PetId id,
                Name name,
                Description description,          
                SpeciesBreedsId speciesBreedsId,
                Color color, string healthInfo,
                Address address,
                PhoneNumber phoneNumber,
                PetCharacteristics petCharacteristics,
                bool isCastrated,
                bool isVaccinated,
                Birthday birthday,
                DateTime publicationDate,
                Photos? photos,
                Requisites? requisites,
                PetStatus status)
                : base(id)
        {
            Name = name;
            Description = description;
            SpeciesBreedsId = speciesBreedsId;
            Color = color;
            HealthInfo = healthInfo;
            Address = address;
            PhoneNumber = phoneNumber;
            PetCharacteristics = petCharacteristics;
            IsCastrated = isCastrated;
            IsVaccinated = isVaccinated;
            Birthday = birthday;
            PublicationDate = publicationDate;
            Photos = photos;
            Requisites = requisites;
            Status = status;
        }
        public Name Name { get; private set; } = null!;
        public Description Description { get; private set; } = null!;
        public SpeciesBreedsId SpeciesBreedsId { get; private set; }
        public Color Color { get; private set; }
        public string HealthInfo { get; private set; } = null!;
        public Address Address { get; private set; } = default!;
        public PhoneNumber PhoneNumber { get; private set; } = null!;
        public PetCharacteristics PetCharacteristics { get; private set; }
        public bool IsCastrated { get; private set; }
        public bool IsVaccinated { get; private set; }
        public Birthday Birthday { get; private set; }
        public DateTime PublicationDate { get; private set; } = default!;
        public Photos? Photos { get; private set; }
        public Requisites? Requisites { get; private set; }
        public PetStatus Status { get; private set; }  
    }
}
