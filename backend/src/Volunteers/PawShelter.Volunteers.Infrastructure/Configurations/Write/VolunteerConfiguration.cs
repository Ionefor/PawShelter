using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawShelter.Core.Dto;
using PawShelter.Core.Extensions;
using PawShelter.SharedKernel.Definitions;
using PawShelter.SharedKernel.ValueObjects;
using PawShelter.SharedKernel.ValueObjects.Ids;
using PawShelter.Volunteers.Contracts.Dto.Command;
using PawShelter.Volunteers.Domain.Aggregate;

namespace PawShelter.Volunteers.Infrastructure.Configurations.Write;

public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteers");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Id).HasConversion(
            id => id.Id,
            value => VolunteerId.Create(value));

        builder.OwnsOne(v => v.FullName, vf =>
        {
            vf.ToJson("full_name");

            vf.Property(vf => vf.FirstName).IsRequired().
                HasMaxLength(Constants.Shared.MaxLowTextLength).
                HasColumnName("first_name");

            vf.Property(vf => vf.MiddleName).
                IsRequired().
                HasMaxLength(Constants.Shared.MaxLowTextLength).
                HasColumnName("middle_name");

            vf.Property(vf => vf.LastName).
                IsRequired().
                HasMaxLength(Constants.Shared.MaxLowTextLength).
                HasColumnName("last_name");
        });

        builder.ComplexProperty(v => v.Email,
            ve =>
            {
                ve.Property(ve => ve.Value).IsRequired().
                    HasMaxLength(Constants.Shared.MaxLowTextLength)
                    .HasColumnName("email");
            });

        builder.ComplexProperty(v => v.Description,
            vd =>
            {
                vd.Property(d => d.Value).IsRequired().
                    HasMaxLength(Constants.Shared.MaxLowTextLength)
                    .HasColumnName("description");
            });

        builder.ComplexProperty(v => v.PhoneNumber,
            vn =>
            {
                vn.Property(n => n.Value).IsRequired().
                    HasMaxLength(Constants.Shared.MaxLowTextLength)
                    .HasColumnName("phone_number");
            });

        builder.ComplexProperty(v => v.Experience,
            vn =>
            {
                vn.Property(e => e.Value).IsRequired().
                    HasMaxLength(Constants.Shared.MaxLowTextLength)
                    .HasColumnName("experience");
            });

        builder.OwnsOne(v => v.Requisites, rb =>
        {
            rb.Property(ph => ph.Values)!.
                ValueObjectsCollectionJsonConversion(
                    requisite => new RequisiteDto(requisite.Name, requisite.Description),
                    dto => Requisite.Create(dto.Name, dto.Description).Value).
                HasColumnName("requisites");
        });
        
        builder.OwnsOne(v => v.SocialNetworks, sb =>
        {
            sb.Property(ph => ph.Values)!.
                ValueObjectsCollectionJsonConversion(
                    socialNetwork => new SocialNetworkDto(socialNetwork.Name, socialNetwork.Url),
                    dto => SocialNetwork.Create(dto.Name, dto.Link).Value).
                HasColumnName("social_networks");
        });

        builder.HasMany(v => v.Pets).
            WithOne().
            HasForeignKey("volunteer_id").
            OnDelete(DeleteBehavior.Cascade).
            IsRequired();

        builder.Property<bool>("_isDeleted").UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
    }
}