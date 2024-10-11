using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawShelter.Domain.SpeciesManagement.Entities;
using PawShelter.Domain.SpeciesManagement.ValueObjects.Ids;

namespace PawShelter.Infrastructure.Configurations
{
    public class BreedConfiguration : IEntityTypeConfiguration<Breed>
    {
        public void Configure(EntityTypeBuilder<Breed> builder)
        {
            builder.ToTable("Breeds");

            builder.HasKey(b => b.Id);

            builder.Property(p => p.Id).
                HasConversion(
                    BreedId => BreedId.Value,
                    value => BreedId.Create(value));

            builder.Property(p => p.Value).
                IsRequired().
                HasColumnName("breed");
        }
    }
}
