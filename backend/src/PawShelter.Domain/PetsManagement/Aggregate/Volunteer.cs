using PawShelter.Domain.PetsManagement.Entities;
using PawShelter.Domain.PetsManagement.ValueObjects.ForVolunteer;
using PawShelter.Domain.PetsManagement.ValueObjects.Ids;
using PawShelter.Domain.PetsManagement.ValueObjects.Shared;
using PawShelter.Domain.Shared;

namespace PawShelter.Domain.PetsManagement.Aggregate
{
    public class Volunteer : Entity<VolunteerId>
    {
        private readonly List<Pet> _pets = [];
        private Volunteer(VolunteerId id) : base(id) { }
        public Volunteer(VolunteerId id,
                FullName fullName,
                Email email,
                Description description,
                PhoneNumber phoneNumber,
                Experience experience,
                Requisites? requisites,
                SocialNetworks? socialNetworks)
                : base(id)
        {
            FullName = fullName;
            Email = email;
            Description = description;
            PhoneNumber = phoneNumber;
            Experience = experience;
            Requisites = requisites;
            SocialNetworks = socialNetworks;
        }
        public FullName FullName { get; private set; }
        public Email Email { get; private set; }
        public Description Description { get; private set; } = null!;
        public PhoneNumber PhoneNumber { get; private set; } = null!;
        public Experience Experience { get; private set; }
        public Requisites? Requisites { get; private set; }
        public SocialNetworks? SocialNetworks { get; private set; }    
        public IReadOnlyList<Pet>? Pets => _pets;
        public int CountPetsByStatus(PetStatus status)
        {
            return _pets.Count(p => p.Status == status);
        }
    }
}

