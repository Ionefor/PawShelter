using CSharpFunctionalExtensions;
using PawShelter.Domain.Shared;

namespace PawShelter.Domain.VolunteerModel
{
    public record Experience
    {
        private Experience() { }
        private Experience(int experience) => Value = experience;
        public int Value { get; }
        public Result<Experience, Error> Create(int experience)
        {
            if (experience < 0 || experience > 100)
                return Errors.General.ValueIsInvalid("Experience");

            return new Experience(experience);
        }
    }
}
