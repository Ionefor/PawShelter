using Microsoft.Extensions.DependencyInjection;
using PawShelter.Application.Volunteers.CreateVolunteer;

namespace PawShelter.Application
{
    public static class Inject
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<CreateVolunteerHandler>();

            return services;
        }
    }
}
