using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawShelter.Application.Dto;

namespace PawShelter.Infrastructure.Configurations.Read;

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