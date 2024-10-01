using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawShelter.Domain.Pets;
using PawShelter.Domain.PetsModel;
using PawShelter.Domain.PetsModel.Ids;

namespace PawShelter.Infrastructure.Configurations
{
    public class BreedConfiguration : IEntityTypeConfiguration<Breed>
    {
        public void Configure(EntityTypeBuilder<Breed> builder)
        {
            builder.ToTable("Breeds");

            builder.HasKey(b => b.Id);

            builder.Property(p => p.Id).HasConversion(
                id => id.Value,
                value => BreedId.Create(value));

            builder.Property(p => p.Value).
                IsRequired().
                HasColumnName("breeds");
        }
    }
}
