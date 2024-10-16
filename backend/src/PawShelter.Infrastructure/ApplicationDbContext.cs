﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PawShelter.Domain.PetsManagement.Aggregate;
using PawShelter.Domain.SpeciesManagement.Aggregate;
using PawShelter.Infrastructure.Interceptors;

namespace PawShelter.Infrastructure
{
    public class ApplicationDbContext(IConfiguration configuration) : DbContext
    {
        private const string DATABASE = "Database";
        public DbSet<Volunteer> Volunteers => Set<Volunteer>();
        public DbSet<Species> Species => Set<Species>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString(DATABASE));
            optionsBuilder.UseSnakeCaseNamingConvention();
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseLoggerFactory(CreateLoggerFactory());

            optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
        private ILoggerFactory CreateLoggerFactory() =>
            LoggerFactory.Create(builder => { builder.AddConsole(); });
    }
}
