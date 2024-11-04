using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawShelter.Application.Dto;
using PawShelter.Domain.PetsManagement.Aggregate;
using PawShelter.Domain.PetsManagement.ValueObjects.ForVolunteer;
using PawShelter.Domain.PetsManagement.ValueObjects.Ids;
using PawShelter.Domain.PetsManagement.ValueObjects.Shared;
using PawShelter.Domain.Shared;
using PawShelter.Infrastructure.Extensions;

namespace PawShelter.Infrastructure.Configurations.Write;

public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteers");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Id).HasConversion(
            id => id.Value,
            value => VolunteerId.Create(value));

        builder.OwnsOne(v => v.FullName, vf =>
        {
            vf.ToJson("full_name");

            vf.Property(vf => vf.FirstName).IsRequired().
                HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH).
                HasColumnName("first_name");

            vf.Property(vf => vf.MiddleName).
                IsRequired().
                HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH).
                HasColumnName("middle_name");

            vf.Property(vf => vf.LastName).
                IsRequired().
                HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH).
                HasColumnName("last_name");
        });

        builder.ComplexProperty(v => v.Email,
            ve =>
            {
                ve.Property(ve => ve.Value).IsRequired().HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("email");
            });

        builder.ComplexProperty(v => v.Description,
            vd =>
            {
                vd.Property(d => d.Value).IsRequired().HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH)
                    .HasColumnName("description");
            });

        builder.ComplexProperty(v => v.PhoneNumber,
            vn =>
            {
                vn.Property(n => n.Value).IsRequired().HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("phone_number");
            });

        builder.ComplexProperty(v => v.Experience,
            vn =>
            {
                vn.Property(e => e.Value).IsRequired().HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
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
                    socialNetwork => new SocialNetworkDto(socialNetwork.Name, socialNetwork.Link),
                    dto => SocialNetwork.Create(dto.Name, dto.Link).Value).
                HasColumnName("social_networks");
        });
        
        // builder.OwnsOne(v => v.SocialNetworks, vs =>
        // {
        //     vs.ToJson("volunteer_socialNetworks");
        //
        //     vs.OwnsMany(vs => vs.Values, r =>
        //     {
        //         r.Property(n => n.Name).IsRequired().HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
        //             .HasColumnName("socialNetwork_name");
        //
        //         r.Property(l => l.Link).IsRequired().HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
        //             .HasColumnName("socialNetwork_link");
        //     });
        // });

        builder.HasMany(v => v.Pets).WithOne().HasForeignKey("volunteer_id").OnDelete(DeleteBehavior.Cascade);

        builder.Property<bool>("_isDeleted").UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
    }
}