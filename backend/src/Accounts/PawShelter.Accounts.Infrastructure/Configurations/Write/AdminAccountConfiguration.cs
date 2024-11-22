﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawShelter.Accounts.Domain.Accounts;

namespace PawShelter.Accounts.Infrastructure.Configurations.Write;

public class AdminAccountConfiguration : IEntityTypeConfiguration<AdminAccount>
{
    public void Configure(EntityTypeBuilder<AdminAccount> builder)
    {
        builder.ToTable("admin_accounts");
        
        builder.HasOne(v => v.User)
            .WithOne()
            .HasForeignKey<AdminAccount>(v => v.UserId);
    }
}