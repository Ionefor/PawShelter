using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawShelter.SharedKernel.ValueObjects;
using PawShelter.Species.Domain.Entities;

namespace PawShelter.Species.Infrastructure.Configurations.Write;

public class BreedConfiguration : IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.ToTable("breeds");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id).HasConversion(
            breedId => breedId.Id,
            value => BreedId.Create(value)).HasColumnName("breed_id");

        builder.Property(p => p.Value).
            HasColumnName("breed").
            IsRequired();
    }
}