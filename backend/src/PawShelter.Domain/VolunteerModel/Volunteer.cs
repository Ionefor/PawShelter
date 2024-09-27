using PawShelter.Domain.Enums;
using PawShelter.Domain.Pets;
using PawShelter.Domain.Shared;

namespace PawShelter.Domain.VolunteerModel
{
    public class Volunteer
    {
        private readonly List<Requisites> _requisites = [];
        private readonly List<Pet> _pets = [];
        private readonly List<SocialNetwork> _socialNetworks = [];
        public Guid Id { get; private set; } 
        public string FirstName { get; private set; } = null!;
        public string MiddleName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public string PhoneNumber { get; private set; } = null!;
        public int YearsOfExperience { get; private set; }
        public IReadOnlyList<Requisites> Requisites => _requisites;
        public IReadOnlyList<Pet> Pets => _pets;
        public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;
        public int CountPetsByStatus(PetStatus status)
        {
            return _pets.Where(p => p.Status == status).Count();
        }
    }
}

