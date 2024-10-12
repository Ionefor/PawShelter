using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawShelter.Domain.SpeciesManagement.Aggregate;
using PawShelter.Domain.SpeciesManagement.ValueObjects.Ids;

namespace PawShelter.Infrastructure.Configurations
{
    public class SpeciesConfiguration : IEntityTypeConfiguration<Species>
    {
        public void Configure(EntityTypeBuilder<Species> builder)
        {
            builder.ToTable("Species");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id).
                HasConversion(
                    speciesId =>  speciesId.Value,
                    value => SpeciesId.Create(value));

            builder.Property(s => s.Value).
                IsRequired().
                HasColumnName("species");

            builder.HasMany(s => s.Breeds).
                WithOne().
                HasForeignKey("species_id").
                OnDelete(DeleteBehavior.Cascade);
        }
    }
}
