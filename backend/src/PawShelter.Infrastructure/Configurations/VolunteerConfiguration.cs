using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawShelter.Domain.PetsModel;
using PawShelter.Domain.Shared.ValueObjects;
using PawShelter.Domain.VolunteerModel;

namespace PawShelter.Infrastructure.Configurations
{
    public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
    {
        public void Configure(EntityTypeBuilder<Volunteer> builder)
        {

            builder.ToTable("Volunteers");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id).
                HasConversion(
                    id => id.Value,
                    value => VolunteerId.Create(value));

            builder.OwnsOne(v => v.FullName, vf =>
            {
                vf.ToJson("full_name");

                vf.Property(vf => vf.FirstName).
                    HasConversion(
                        name => name.Value,
                        value => Name.Create(value).Value).
                    IsRequired().
                    HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH);

                vf.Property(vf => vf.MiddleName).
                    HasConversion(
                        name => name.Value,
                        value => Name.Create(value).Value).
                    IsRequired().
                    HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH);

                vf.Property(vf => vf.LastName).
                    HasConversion(
                        name => name.Value,
                        value => Name.Create(value).Value).
                    IsRequired().
                    HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH);
            });

            builder.ComplexProperty(v => v.Email, ve =>
            {
                ve.Property(ve => ve.Value).
                    IsRequired().
                    HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH).
                    HasColumnName("email");
            });

            builder.ComplexProperty(v => v.Description, vd =>
            {
                vd.Property(d => d.Value).
                    IsRequired().
                    HasMaxLength(Domain.Shared.Constants.MAX_HIGH_TEXT_LENGTH).
                    HasColumnName("description");
            });

            builder.ComplexProperty(v => v.PhoneNumber, vn =>
            {
                vn.Property(n => n.Value).
                    IsRequired().
                    HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH).
                    HasColumnName("phoneNumber");
            });

            builder.ComplexProperty(v => v.Experience, vn =>
            {
                vn.Property(e => e.Value).
                    IsRequired().
                    HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH).
                    HasColumnName("experience");
            });

            builder.OwnsOne(v => v.Requisites, vr =>
            {
                vr.ToJson("volunteer_requisites");

                vr.OwnsMany(rs => rs.Values, r =>
                {
                    r.Property(n => n.Name).
                    HasConversion(
                        name => name.Value,
                        value => Name.Create(value).Value).
                    IsRequired().
                    HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH);

                    r.Property(d => d.Description).
                    HasConversion(
                        description => description.Value,
                        value => Description.Create(value).Value).
                    IsRequired().
                    HasMaxLength(Domain.Shared.Constants.MAX_HIGH_TEXT_LENGTH);
                });
            });

            builder.OwnsOne(v => v.SocialNetworks, vs =>
            {
                vs.ToJson("volunteer_socialNetworks");

                vs.OwnsMany(vs => vs.Values, r =>
                {
                    r.Property(n => n.Name).
                    HasConversion(
                        name => name.Value,
                        value => Name.Create(value).Value).
                    IsRequired().
                    HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH);

                    r.Property(l => l.Link).
                        IsRequired().
                        HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH);
                });
            });

            builder.HasMany(v => v.Pets).
                WithOne().
                HasForeignKey("volunteer_id").
                OnDelete(DeleteBehavior.NoAction);
        }
    }
}
