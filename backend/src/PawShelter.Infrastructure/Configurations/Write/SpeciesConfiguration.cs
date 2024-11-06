using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawShelter.Domain.SpeciesManagement.Aggregate;
using PawShelter.Domain.SpeciesManagement.ValueObjects.Ids;

namespace PawShelter.Infrastructure.Configurations.Write;

public class SpeciesConfiguration : IEntityTypeConfiguration<Species>
{
    public void Configure(EntityTypeBuilder<Species> builder)
    {
        builder.ToTable("species");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id).HasConversion(
            speciesId => speciesId.Value,
            value => SpeciesId.Create(value)).HasColumnName("species_id");

        builder.Property(s => s.Value).HasColumnName("species").IsRequired();

        builder.HasMany(s => s.Breeds).
            WithOne().
            HasForeignKey("species_id").
            OnDelete(DeleteBehavior.Cascade).
            IsRequired();
    }
}