using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawShelter.Application.Dto;
using PawShelter.Domain.PetsManagement.Entities;
using PawShelter.Domain.PetsManagement.ValueObjects.ForPet;
using PawShelter.Domain.PetsManagement.ValueObjects.Ids;
using PawShelter.Domain.PetsManagement.ValueObjects.Shared;
using PawShelter.Domain.Shared;
using PawShelter.Domain.SpeciesManagement.ValueObjects.Ids;
using PawShelter.Infrastructure.Extensions;

namespace PawShelter.Infrastructure.Configurations.Write;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).HasConversion(
            id => id.Value,
            value => PetId.Create(value));

        builder.ComplexProperty(p => p.Name,
            pn =>
            {
                pn.Property(n => n.Value).IsRequired().HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("name");
            });

        builder.ComplexProperty(p => p.Description,
            pd =>
            {
                pd.Property(d => d.Value).IsRequired().HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH)
                    .HasColumnName("description");
            });

        builder.ComplexProperty(p => p.SpeciesBreedsId, psb =>
        {
            psb.Property(p => p.SpeciesId).HasConversion(
                id => id.Value,
                value => SpeciesId.Create(value)).HasColumnName("species_id");

            psb.Property(b => b.BreedId).IsRequired().HasColumnName("breed_id");
        });

        builder.ComplexProperty(p => p.Color,
            pc =>
            {
                pc.Property(c => c.Value).IsRequired().HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("color");
            });

        builder.ComplexProperty(p => p.HealthInfo,
            phi => { phi.Property(hi => hi.Value).
                IsRequired().HasColumnName("health_info"); });

        builder.ComplexProperty(p => p.Address, pa =>
        {
            pa.Property(a => a.Country).IsRequired().HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("country");

            pa.Property(a => a.City).IsRequired().HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH).HasColumnName("city");

            pa.Property(a => a.Street).IsRequired().HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH).HasColumnName("street");

            pa.Property(a => a.HouseNumber).IsRequired().HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("house_number");
        });

        builder.ComplexProperty(p => p.PetCharacteristics, pc =>
        {
            pc.Property(c => c.Weight).
                IsRequired().
                HasMaxLength(
                    Constants.MAX_LOW_TEXT_LENGTH).HasColumnName("weight");

            pc.Property(c => c.Height).
                IsRequired().
                HasMaxLength(
                    Constants.MAX_LOW_TEXT_LENGTH).HasColumnName("height");
        });

        builder.ComplexProperty(p => p.PhoneNumber,
            pn =>
            {
                pn.Property(n => n.Value).IsRequired().HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("phone_number");
            });

        builder.Property(p => p.IsCastrated).IsRequired();

        builder.Property(p => p.IsVaccinated).IsRequired();

        builder.ComplexProperty(p => p.Position,
            pb => { pb.Property(b => b.Value).IsRequired().HasColumnName("position"); });

        builder.ComplexProperty(p => p.Birthday,
            pb => { pb.Property(b => b.Value).IsRequired().HasColumnName("birthday"); });

        builder.Property(p => p.PublicationDate).IsRequired();
        
        builder.Property(p => p.Photos)!.
            ValueObjectsCollectionJsonConversion(
                photo => new PetPhotoDto(photo.Path.Path, photo.IsMain),
                dto => new PetPhoto(FilePath.Create(dto.Path).Value, dto.IsMain)).
            HasColumnName("photos");      
        
        
        builder.OwnsOne(p => p.Requisites, pr =>
        {
            pr.Property(ph => ph.Values)!.
                ValueObjectsCollectionJsonConversion(
                    requisite => new RequisiteDto(requisite.Name, requisite.Description),
                    dto => Requisite.Create(dto.Name, dto.Description).Value).
                HasColumnName("requisites");
        });

        builder.Property(p => p.Status).IsRequired();

        builder.Property<bool>("_isDeleted").UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
    }
}