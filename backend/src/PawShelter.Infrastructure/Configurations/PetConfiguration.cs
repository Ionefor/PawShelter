using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawShelter.Domain.Pets;
using PawShelter.Domain.PetsModel.Ids;
using PawShelter.Domain.Shared.ValueObjects;

namespace PawShelter.Infrastructure.Configurations
{
    public class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.ToTable("Pets");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).
                HasConversion(
                    id => id.Value,
                    value => PetId.Create(value));

            builder.ComplexProperty(p => p.Name, pn =>
            {
                pn.Property(n => n.Value).
                    IsRequired().
                    HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH).
                    HasColumnName("name");
            });

            builder.ComplexProperty(p => p.Description, pd =>
            {
                pd.Property(d => d.Value).
                    IsRequired().
                    HasMaxLength(Domain.Shared.Constants.MAX_HIGH_TEXT_LENGTH).
                    HasColumnName("description");
            });

            builder.ComplexProperty(p => p.SpeciesBreedsId, psb =>
            {
                psb.Property(p => p.SpeciesId).
                    HasConversion(
                        id => id.Value,
                        value => SpeciesId.Create(value));
                
                psb.Property(b => b.BreedId).
                    IsRequired().
                    HasColumnName("breed");
            });

            builder.ComplexProperty(p => p.Color, pc =>
            {
                pc.Property(c => c.Value).
                    IsRequired().
                    HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH).
                    HasColumnName("color");
            });

            builder.ComplexProperty(p => p.HealthInfo, phi =>
            {
                phi.Property(hi => hi.Value).
                    IsRequired().
                    HasColumnName("health_Info");
            });

            builder.ComplexProperty(p => p.Address, pa =>
            {
                pa.Property(a => a.Country).
                    IsRequired().
                    HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH).
                    HasColumnName("country");

                pa.Property(a => a.City).
                    IsRequired().
                    HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH).
                    HasColumnName("city");

                pa.Property(a => a.Street).
                    IsRequired().
                    HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH).
                    HasColumnName("street");

                pa.Property(a => a.HouseNumber).
                    IsRequired().
                    HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH).
                    HasColumnName("houseNumber");
            });

            builder.ComplexProperty(p => p.PetCharacteristics, pc =>
            {
                pc.Property(c => c.Width).
                    IsRequired().
                    HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH).
                    HasColumnName("width");

                pc.Property(c => c.Height).
                    IsRequired().
                    HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH).
                    HasColumnName("height");
            });

            builder.ComplexProperty(p => p.PhoneNumber, pn =>
            {
                pn.Property(n => n.Value).
                    IsRequired().
                    HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH).
                    HasColumnName("phoneNumber");
            });

            builder.Property(p => p.IsCastrated).
                IsRequired();

            builder.Property(p => p.IsVaccinated).
               IsRequired();

            builder.ComplexProperty(p => p.Birthday, pb =>
            {
                pb.Property(b => b.Value).
                    IsRequired().
                    HasColumnName("birthday");
            });

            builder.Property(p => p.PublicationDate).
               IsRequired();

            builder.OwnsOne(p => p.Photos, pp =>
            {
                pp.ToJson("pet_photos");

                pp.OwnsMany(phs => phs.Values, ph =>
                {                 
                    ph.Property(h => h.Path)
                        .IsRequired();

                    ph.Property(h => h.IsMain)
                        .IsRequired();                  
                });
            });

            builder.OwnsOne(p => p.Requisites, pr =>
            {
                pr.ToJson("pet_requisites");

                pr.OwnsMany(rs => rs.Values, r =>
                {
                    r.Property(n => n.Name).
                    IsRequired().
                    HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH);

                    r.Property(d => d.Description).
                    IsRequired().
                    HasMaxLength(Domain.Shared.Constants.MAX_HIGH_TEXT_LENGTH);
                });
            });

            builder.Property(p => p.Status).
                IsRequired();
        }
    }
}
