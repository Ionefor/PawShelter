using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PawShelter.Application.Database;
using PawShelter.Application.Messaging;
using PawShelter.Application.PhotoProvider;
using PawShelter.Application.Species;
using PawShelter.Application.Volunteers;
using PawShelter.Infrastructure.DbContexts;
using PawShelter.Infrastructure.MessageQueues;
using PawShelter.Infrastructure.Options;
using PawShelter.Infrastructure.Providers;
using PawShelter.Infrastructure.Repositories;

namespace PawShelter.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContexts().
            AddMinio(configuration).
            AddMessageQueues().
            AddDatabase().
            AddRepositories();

        return services;
    }

    private static IServiceCollection AddMinio(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<MinioOptions>(
            configuration.GetSection(MinioOptions.MINIO));

        services.AddMinio(options =>
        {
            var minioOptions = configuration.GetSection(MinioOptions.MINIO).Get<MinioOptions>()
                               ?? throw new ApplicationException();

            options.WithEndpoint(minioOptions.Endpoint);
            options.WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey);
            options.WithSSL(minioOptions.WithSSL);
        });

        services.AddScoped<IPhotoProvider, MinioProvider>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IVolunteerRepository, VolunteerRepository>();
        services.AddScoped<ISpeciesRepository, SpeciesRepository>();

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
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    private static IServiceCollection AddMessageQueues(this IServiceCollection services)
    {
        services.AddSingleton<IMessageQueue<IEnumerable<PhotoMetaData>>,
            InMemoryMessageQueue<IEnumerable<PhotoMetaData>>>();

        return services;
    }
}