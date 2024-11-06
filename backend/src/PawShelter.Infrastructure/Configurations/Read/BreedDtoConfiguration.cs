using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawShelter.Application.Dto;

namespace PawShelter.Infrastructure.Configurations.Read;

public class BreedDtoConfiguration : IEntityTypeConfiguration<BreedDto>
{
    public void Configure(EntityTypeBuilder<BreedDto> builder)
    {
        builder.ToTable("breeds");
        
        builder.HasKey(b => b.BreedId);
        
        builder.Property(b => b.SpeciesId);

        builder.Property(b => b.Breed);
    }
}