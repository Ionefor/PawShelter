using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PawShelter.Application;
using PawShelter.Application.FileProvider;
using PawShelter.Application.Species;
using PawShelter.Application.Volunteers;
using PawShelter.Infrastructure.Options;
using PawShelter.Infrastructure.Providers;
using PawShelter.Infrastructure.Repositories;

namespace PawShelter.Infrastructure
{
    public static class Inject
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<IVolunteerRepository, VolunteerRepository>();
            services.AddScoped<ISpeciesRepository, SpeciesRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddMinio(configuration);
           
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
            
            services.AddScoped<IFileProvider, MinioProvider>();
            
            return services;
        }
    }
}
