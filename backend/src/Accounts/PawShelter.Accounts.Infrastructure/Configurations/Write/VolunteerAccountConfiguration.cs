using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawShelter.Accounts.Domain.Accounts;
using PawShelter.Core.Dto;
using PawShelter.Core.Extensions;
using PawShelter.SharedKernel.ValueObjects;

namespace PawShelter.Accounts.Infrastructure.Configurations.Write;

public class VolunteerAccountConfiguration : IEntityTypeConfiguration<VolunteerAccount>
{
    public void Configure(EntityTypeBuilder<VolunteerAccount> builder)
    {
        builder.ToTable("volunteer_accounts");

        builder.ComplexProperty(v => v.Experience, eb =>
        {
            eb.Property(e => e.Value)
                .IsRequired()
                .HasColumnName("experience");
        });
        
        builder.Property(v => v.Requisites)
            .ValueObjectsCollectionJsonConversion<Requisite, RequisiteDto>(
                requisite => new RequisiteDto(requisite.Name, requisite.Description),
                dto => Requisite.Create(dto.Name, dto.Description).Value)
            .HasColumnName("requisites");
        
        builder.HasOne(v => v.User)
            .WithOne()
            .HasForeignKey<VolunteerAccount>(v => v.UserId);
    }
}