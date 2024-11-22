using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PawShelter.Core.Abstractions;
using PawShelter.Core.Messaging;
using PawShelter.SharedKernel.Definitions;
using PawShelter.Volunteers.Application;
using PawShelter.Volunteers.Application.PhotoProvider;
using PawShelter.Volunteers.Application.Volunteers;
using PawShelter.Volunteers.Infrastructure.DbContexts;
using PawShelter.Volunteers.Infrastructure.MessageQueues;
using PawShelter.Volunteers.Infrastructure.Options;
using PawShelter.Volunteers.Infrastructure.Providers;
using PawShelter.Volunteers.Infrastructure.Repositories;
using ServiceCollectionExtensions = Minio.ServiceCollectionExtensions;

namespace PawShelter.Volunteers.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddVolunteersInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContexts().
            AddFilesService(configuration).
            AddMessageQueues().
            AddDatabase().
            AddRepositories();

        return services;
    }
    private static IServiceCollection AddFilesService(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<MinioOptions>(
            configuration.GetSection(MinioOptions.Minio));

        ServiceCollectionExtensions.AddMinio(services, options =>
        {
            var minioOptions = configuration.GetSection(MinioOptions.Minio).Get<MinioOptions>()
                               ?? throw new ApplicationException();

            options.WithEndpoint(minioOptions.Endpoint);
            options.WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey);
            options.WithSSL(minioOptions.WithSsl);
        });

        services.AddScoped<IPhotoProvider, MinioProvider>();

        return services;
    }
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IVolunteerRepository, VolunteerRepository>();

        return services;
    }
    private static IServiceCollection AddDbContexts(this IServiceCollection services)
    {
        services.AddScoped<WriteDbContext>();
        services.AddScoped<IReadDbContext, ReadDbContext>();

        return services;
    }
    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(ModulesName.Volunteers);

        return services;
    }
    private static IServiceCollection AddMessageQueues(this IServiceCollection services)
    {
        services.AddSingleton<IMessageQueue<IEnumerable<PhotoMetaData>>,
            InMemoryMessageQueue<IEnumerable<PhotoMetaData>>>();

        return services;
    }
}