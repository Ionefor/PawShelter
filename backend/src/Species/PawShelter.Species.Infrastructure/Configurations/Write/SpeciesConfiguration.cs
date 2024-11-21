using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawShelter.SharedKernel.ValueObjects;
using PawShelter.SharedKernel.ValueObjects.Ids;

namespace PawShelter.Species.Infrastructure.Configurations.Write;

public class SpeciesConfiguration : IEntityTypeConfiguration<Species.Domain.Aggregate.Species>
{
    public void Configure(EntityTypeBuilder<Species.Domain.Aggregate.Species> builder)
    {
        builder.ToTable("species");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id).HasConversion(
            speciesId => speciesId.Id,
            value => SpeciesId.Create(value)).HasColumnName("species_id");
        
        builder.Property(s => s.Value).HasColumnName("species").IsRequired();

        builder.HasMany(s => s.Breeds).
            WithOne().
            HasForeignKey("species_id").
            OnDelete(DeleteBehavior.Cascade).
            IsRequired();
    }
}