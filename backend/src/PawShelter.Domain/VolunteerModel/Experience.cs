using PawShelter.Domain.Shared;

namespace PawShelter.Domain.VolunteerModel
{
    public record Experience
    {
        private Experience() { }
        private Experience(int experience) =>
             Value = experience;
        public int Value { get; }
        public Result<Experience> Create(int experience)
        {
            if (experience < 0 || experience > 100)
                return "Invalid experience";

            return new Experience(experience);
        }
    }
}
