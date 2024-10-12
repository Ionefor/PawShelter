using Microsoft.Extensions.DependencyInjection;
using PawShelter.Application.Volunteers;
using PawShelter.Infrastructure.Repositories;

namespace PawShelter.Infrastructure
{
    public static class Inject
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<IVolunteerRepository, VolunteerRepository>();

            return services;
        }
    }
}
