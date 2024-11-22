using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawShelter.Core.Dto;
using PawShelter.Core.Extensions;
using PawShelter.SharedKernel.Definitions;
using PawShelter.SharedKernel.ValueObjects;
using PawShelter.SharedKernel.ValueObjects.Ids;
using PawShelter.Volunteers.Contracts.Dto.Command;
using PawShelter.Volunteers.Contracts.Dto.Models;
using PawShelter.Volunteers.Domain.Entities;
using PawShelter.Volunteers.Domain.ValueObjects;

namespace PawShelter.Volunteers.Infrastructure.Configurations.Write;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).HasConversion(
            id => id.Id,
            value => PetId.Create(value));

        builder.ComplexProperty(p => p.Name,
            pn =>
            {
                pn.Property(n => n.Value).IsRequired().HasMaxLength(
                        Constants.Shared.MaxLowTextLength)
                    .HasColumnName("name");
            });

        builder.ComplexProperty(p => p.Description,
            pd =>
            {
                pd.Property(d => d.Value).IsRequired().HasMaxLength(Constants.Shared.MaxLowTextLength)
                    .HasColumnName("description");
            });

        builder.ComplexProperty(p => p.SpeciesBreedsId, psb =>
        {
            psb.Property(p => p.SpeciesId).HasConversion(
                id => id.Id,
                value => SpeciesId.Create(value)).HasColumnName("species_id");

            psb.Property(b => b.BreedId).IsRequired().HasColumnName("breed_id");
        });

        builder.ComplexProperty(p => p.Color,
            pc =>
            {
                pc.Property(c => c.Value).IsRequired().
                    HasMaxLength(Constants.Shared.MaxLowTextLength)
                    .HasColumnName("color");
            });

        builder.ComplexProperty(p => p.HealthInfo,
            phi => { phi.Property(hi => hi.Value).
                IsRequired().HasColumnName("health_info"); });

        builder.ComplexProperty(p => p.Address, pa =>
        {
            pa.Property(a => a.Country).IsRequired().
                HasMaxLength(Constants.Shared.MaxLowTextLength)
                .HasColumnName("country");

            pa.Property(a => a.City).IsRequired().
                HasMaxLength(Constants.Shared.MaxLowTextLength).HasColumnName("city");

            pa.Property(a => a.Street).IsRequired().
                HasMaxLength(Constants.Shared.MaxLowTextLength).HasColumnName("street");

            pa.Property(a => a.HouseNumber).IsRequired().
                HasMaxLength(Constants.Shared.MaxLowTextLength)
                .HasColumnName("house_number");
        });

        builder.ComplexProperty(p => p.PetCharacteristics, pc =>
        {
            pc.Property(c => c.Weight).
                IsRequired().
                HasMaxLength(
                    Constants.Shared.MaxLowTextLength).HasColumnName("weight");

            pc.Property(c => c.Height).
                IsRequired().
                HasMaxLength(
                    Constants.Shared.MaxLowTextLength).HasColumnName("height");
        });

        builder.ComplexProperty(p => p.PhoneNumber,
            pn =>
            {
                pn.Property(n => n.Value).IsRequired().
                    HasMaxLength(Constants.Shared.MaxLowTextLength)
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
                photo => new PetPhotoDto(photo.Path.Value, photo.IsMain),
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