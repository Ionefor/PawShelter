using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PawShelter.Application.Volunteers.CreateVolunteer;

namespace PawShelter.Application
{
    public static class Inject
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<CreateVolunteerHandler>();
            
            services.AddValidatorsFromAssembly(typeof(Inject).Assembly);
            
            return services;
        }
    }
}
