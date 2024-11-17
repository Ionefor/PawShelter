using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PawShelter.Accounts.Domain;
using PawShelter.Accounts.Domain.Accounts;
using PawShelter.SharedKernel.ValueObjects;

namespace PawShelter.Accounts.Infrastructure.DbContexts;

public class AccountDbContext(IConfiguration configuration) : IdentityDbContext<User, Role, Guid>
{
    private const string DATABASE = "Database";
    
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<RolePermission> RolePermission => Set<RolePermission>();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(DATABASE));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().
            ToTable("users");
        
        modelBuilder.Entity<User>().Property(u => u.SocialNetworks).
            HasConversion(
                u => JsonSerializer.Serialize(u, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<List<SocialNetwork>>(json, JsonSerializerOptions.Default)!);

        modelBuilder.Entity<User>().
            Property(u => u.Photo);
        
        modelBuilder.Entity<User>().OwnsOne(v => v.FullName, vf =>
        {
            vf.ToJson("full_name");

            vf.Property(vf => vf.FirstName).IsRequired().
                HasMaxLength(SharedKernel.Constants.MAX_LOW_TEXT_LENGTH).
                HasColumnName("first_name");

            vf.Property(vf => vf.MiddleName).
                IsRequired().
                HasMaxLength(SharedKernel.Constants.MAX_LOW_TEXT_LENGTH).
                HasColumnName("middle_name");

            vf.Property(vf => vf.LastName).
                IsRequired().
                HasMaxLength(SharedKernel.Constants.MAX_LOW_TEXT_LENGTH).
                HasColumnName("last_name");
        });
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.Roles)
            .WithMany()
            .UsingEntity<IdentityUserRole<Guid>>();
        
        modelBuilder.Entity<AdminAccount>()
            .HasOne(a => a.User)
            .WithOne()
            .HasForeignKey<AdminAccount>(a => a.UserId);

        modelBuilder.Entity<VolunteerAccount>()
            .Property(va => va.Experience);
        
        modelBuilder.Entity<VolunteerAccount>().Property(u => u.Certificates).
            HasConversion(
                u => JsonSerializer.Serialize(u, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<List<Description>>(json, JsonSerializerOptions.Default)!);
        
        modelBuilder.Entity<VolunteerAccount>().Property(u => u.Requisites).
            HasConversion(
                u => JsonSerializer.Serialize(u, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<List<Requisite>>(json, JsonSerializerOptions.Default)!);
        
        modelBuilder.Entity<Role>().
            ToTable("roles");
        
        modelBuilder.Entity<Permission>().
            ToTable("permissions");

        modelBuilder.Entity<Permission>().
            HasIndex(p => p.Code).
            IsUnique();
        
        modelBuilder.Entity<Permission>().
            Property(p => p.Description).
            HasMaxLength(300);
        
        modelBuilder.Entity<RolePermission>().
            ToTable("role_permissions");

        modelBuilder.Entity<RolePermission>().
            HasOne(rp => rp.Role).
            WithMany(r => r.RolePermission).
            HasForeignKey(rp => rp.RoleId);
        
        modelBuilder.Entity<RolePermission>().
            HasOne(rp => rp.Permission).
            WithMany().
            HasForeignKey(rp => rp.PermissionId);

        modelBuilder.Entity<RolePermission>().
            HasKey(rp => new { rp.RoleId, rp.PermissionId });
        
        modelBuilder.Entity<RolePermission>()
            .HasKey(rp => new { rp.RoleId, rp.PermissionId });

        modelBuilder.Entity<IdentityUserClaim<Guid>>()
            .ToTable("user_claims");

        modelBuilder.Entity<IdentityUserToken<Guid>>()
            .ToTable("user_tokens");

        modelBuilder.Entity<IdentityUserLogin<Guid>>()
            .ToTable("user_logins");

        modelBuilder.Entity<IdentityUserRole<Guid>>()
            .ToTable("user_roles");
        
        modelBuilder.HasDefaultSchema("accounts");
        
        base.OnModelCreating(modelBuilder);
    }

    private ILoggerFactory CreateLoggerFactory()
    {
        return LoggerFactory.Create(builder => { builder.AddConsole(); });
    }
}