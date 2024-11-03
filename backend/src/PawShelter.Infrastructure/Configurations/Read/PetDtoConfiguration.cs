using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawShelter.Application.Dto;
using PawShelter.Domain.PetsManagement.ValueObjects.ForPet;
using PawShelter.Infrastructure.Extensions;

namespace PawShelter.Infrastructure.Configurations.Read;

public class PetDtoConfiguration : IEntityTypeConfiguration<PetDto>
{
    public void Configure(EntityTypeBuilder<PetDto> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(p => p.Id);

        builder.HasOne<PetDto>().
            WithMany().
            HasForeignKey(p => p.VolunteerId).
            IsRequired(false);
        
        builder.HasOne<PetDto>().
            WithMany().
            HasForeignKey(p => p.SpeciesId).
            IsRequired(false);
        
        builder.HasOne<PetDto>().
            WithMany().
            HasForeignKey(p => p.BreedId).
            IsRequired(false);
        
        builder.Property(b => b.Name);
        
        builder.Property(b => b.Description);

        builder.Property(b => b.PhoneNumber);

        builder.Property(b => b.Status);
        
        builder.ComplexProperty(p => p.Address, ab =>
        {
            ab.Property(a => a.Country);

            ab.Property(a => a.City);

            ab.Property(a => a.Street);

            ab.Property(a => a.HouseNumber);
        });
        
        builder.Property(d => d.Birthday);

        builder.Property(p => p.Weight);

        builder.Property(p => p.Height);

        builder.Property(p => p.HealthInfo)
            .HasColumnName("health_info");

        builder.Property(p => p.Color);

        builder.Property(p => p.IsCastrated)
            .HasColumnName("is_castrated");

        builder.Property(p => p.IsVaccinated)
            .HasColumnName("is_vaccinated");
        
        builder.Property(p => p.Requisites)
            .HasConversion(
                requisites => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<RequisiteDto[]>(json, JsonSerializerOptions.Default)!);
        
        builder.Property(p => p.Photos)
            .HasConversion(
                photos => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<PetPhotoDto[]>(json, JsonSerializerOptions.Default)!);
    }
}