using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawShelter.Core.Dto;
using PawShelter.Species.Contracts.Dto;

namespace PawShelter.Species.Infrastructure.Configurations.Read;

public class SpeciesDtoConfiguration : IEntityTypeConfiguration<SpeciesDto>
{
    public void Configure(EntityTypeBuilder<SpeciesDto> builder)
    {
        builder.ToTable("species");
        
        builder.HasKey(s => s.SpeciesId);

        builder.Property(s => s.Species);

        builder.HasMany(s => s.Breeds).
            WithOne().
            HasForeignKey(b => b.SpeciesId).
            IsRequired();
    }
}