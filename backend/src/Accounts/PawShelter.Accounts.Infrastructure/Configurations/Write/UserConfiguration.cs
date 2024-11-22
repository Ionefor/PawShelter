using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawShelter.Accounts.Domain;
using PawShelter.Core.Extensions;
using PawShelter.SharedKernel.Definitions;
using PawShelter.SharedKernel.ValueObjects;
using PawShelter.Volunteers.Contracts.Dto.Command;

namespace PawShelter.Accounts.Infrastructure.Configurations.Write;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity<IdentityUserRole<Guid>>();

        builder.Property(u => u.SocialNetworks)
            .ValueObjectsCollectionJsonConversion<SocialNetwork, SocialNetworkDto>(
                socialNetwork => new SocialNetworkDto(socialNetwork.Name, socialNetwork.Url),
                dto => SocialNetwork.Create(dto.Name, dto.Link).Value)
            .HasColumnName("social_networks");
        
        builder.ComplexProperty(v => v.FullName, fb =>
        {
            fb.Property(f => f.FirstName)
                .IsRequired()
                .HasMaxLength(Constants.Shared.MaxLowTextLength)
                .HasColumnName("firstName");

            fb.Property(f => f.MiddleName)
                .IsRequired()
                .HasMaxLength(Constants.Shared.MaxLowTextLength)
                .HasColumnName("middleName");

            fb.Property(f => f.LastName)
                .IsRequired(false)
                .HasMaxLength(Constants.Shared.MaxLowTextLength)
                .HasColumnName("lastName");
        });
    }
}