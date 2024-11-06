using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawShelter.Domain.SpeciesManagement.Entities;
using PawShelter.Domain.SpeciesManagement.ValueObjects.Ids;

namespace PawShelter.Infrastructure.Configurations.Write;

public class BreedConfiguration : IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.ToTable("breeds");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id).HasConversion(
            breedId => breedId.Value,
            value => BreedId.Create(value)).HasColumnName("breed_id");

        builder.Property(p => p.Value).
            HasColumnName("breed").
            IsRequired();
    }
}