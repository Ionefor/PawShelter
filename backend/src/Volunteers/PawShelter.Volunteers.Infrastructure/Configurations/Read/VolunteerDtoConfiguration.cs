using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawShelter.Core.Dto;

namespace PawShelter.Volunteers.Infrastructure.Configurations.Read;

public class VolunteerDtoConfiguration : IEntityTypeConfiguration<VolunteerDto>
{
    public void Configure(EntityTypeBuilder<VolunteerDto> builder)
    {
        builder.ToTable("volunteers");

        builder.HasKey(v => v.Id);

        builder.HasMany(v => v.Pets).WithOne().HasForeignKey(v => v.VolunteerId);

        builder.OwnsOne(v => v.FullName, fnb =>
        {
            fnb.ToJson("full_name");
            
            fnb.Property(f => f.FirstName).
                HasColumnName("first_name");
            
            fnb.Property(f => f.MiddleName).
                HasColumnName("middle_name");
            
            fnb.Property(f => f.LastName).
                HasColumnName("last_name");
        });
        
        builder.Property(v => v.Description);
        
        builder.Property(v => v.Email);

        builder.Property(v => v.Experience);

        builder.Property(v => v.PhoneNumber);

        builder.Property(i => i.SocialNetworks)
            .HasConversion(
                requisites => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<SocialNetworkDto[]>(json, JsonSerializerOptions.Default)!);

        builder.Property(i => i.Requisites)
            .HasConversion(
                requisites => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<RequisiteDto[]>(json, JsonSerializerOptions.Default)!);
        
        builder.Property(p => p.IsDeleted)
            .HasColumnName("is_deleted");
    }
}