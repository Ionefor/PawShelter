using FluentValidation;
using Microsoft.OpenApi.Models;
using PawShelter.Accounts.Application;
using PawShelter.Accounts.Infrastructure;
using PawShelter.Accounts.Presentation;
using PawShelter.Species.Application;
using PawShelter.Species.Infrastructure;
using PawShelter.Species.Presentation;
using PawShelter.Volunteers.Application;
using PawShelter.Volunteers.Infrastructure;
using PawShelter.Volunteers.Presentation;
using Serilog;
using Serilog.Events;

namespace PawShelter.Web.Extensions;

public static class AddServicesExtension
{
     public static IServiceCollection AddServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddVolunteersModule(configuration).
            AddSpeciesModule(configuration).
            AddAccountsModule(configuration)
            .AddCustomSwaggerGen().
            AddValidation(configuration).
            AddLogging(configuration);

        return services;
    }
    
    private static IServiceCollection AddValidation(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddValidatorsFromAssembly(typeof(Program).Assembly);

        return services;
    }
    
    private static IServiceCollection AddCustomSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "PawShelter API",
                Version = "1"
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Insert JWT token value",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement

            { 
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });

        return services;
    }
    
    private static IServiceCollection AddLogging(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.Debug()
            .WriteTo.Seq(configuration.GetConnectionString("Seq")
                         ?? throw new ArgumentNullException("Seq"))
            .Enrich.WithThreadId()
            .Enrich.WithThreadName()
            .Enrich.WithEnvironmentName()
            .Enrich.WithMachineName()
            .Enrich.WithEnvironmentUserName()
            .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
            .CreateLogger();

        services.AddSerilog();
        services.AddHttpLogging(o => { o.CombineLogs = true; });

        return services;
    }
    
    private static IServiceCollection AddVolunteersModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.
            AddVolunteersInfrastructure(configuration).
            AddVolunteersApplication().
            AddVolunteersPresentation();

        return services;
    }
    
    private static IServiceCollection AddSpeciesModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.
            AddSpeciesInfrastructure(configuration).
            AddSpeciesApplication().
            AddSpeciesPresentation();

        return services;
    }
    
    private static IServiceCollection AddAccountsModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.
            AddAccountsInfrastructure(configuration).
            AddAccountsApplication().
            AddAccountsPresentation();

        return services;
    }
}